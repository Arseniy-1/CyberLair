using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using Project.Scripts.SkillSystem.SkillViews;
using Project.Scripts.StateMashine;
using Project.Scripts.StateMashine.PlayerStates;
using Project.Scripts.Stats;
using Project.Scripts.UI;
using UnityEngine;
using IState = Project.Scripts.Interfaces.IState;

namespace Project.Scripts.PlayerSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour, ITarget, IDamageable, IStunable, IDieable
    {
        private const float TakeDamageImmortalityTime = 0.7f;
    
        [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private PlayerInputProvider _playerInputProvider;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Jumper _jumper;
        [SerializeField] private TargetScanner _targetScanner;
        [SerializeField] private Destroyer _destroyer;
        [SerializeField] private Magnet _magnet;
        [SerializeField] private InjuredScreenView _injuredScreenView;

        [SerializeField] private Animator _animator;

        [SerializeField] private HealthRegenerator _healthRegenerator;
        [SerializeField] private ShieldRegenerator _shieldRegenerator;

        [SerializeField] private Collider2D _collider;

        private bool _isDamaged;
        private Coroutine _immortalityCoroutine;
        private Coroutine _stunCoroutine;
        private EntityStateMachine _entityStateMachine;

        public event Action OnDeath;
    
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Vector2 Position => transform.position;
    
        public ExperienceStorage ExperienceStorage { get; } = new ();
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            _playerInputProvider.OnShootButtonPressed += Shoot;
        }

        private void Update()
        {
            PlayerStats.Update();
            _entityStateMachine.Update();
        }
    
        private void OnDisable()
        {
            _playerInputProvider.OnShootButtonPressed -= Shoot;
        }

        private void InitializeComponents()
        {
            var playerStates = new List<IState>
            {
                new PlayerIdleState(_playerMover, _rigidbody2D, _weaponHolder, _targetScanner),
                new PlayerMoveState(_playerInputProvider, _playerMover, _weaponHolder, _targetScanner, _jumper),
                new PlayerJumpState(_playerInputProvider, _collider, _jumper),
                new PlayerStunnedState(_playerMover, _jumper),
            };

            _entityStateMachine = new EntityStateMachine(playerStates);

            foreach (IState state in playerStates)
            {
                state.Initialize(_entityStateMachine, _animator);
            }

            _entityStateMachine.Initialize();
            PlayerStats.Initialize();
            _destroyer.Initialize(PlayerStats.Health, this);
            _playerMover.Initialize(_playerInputProvider, _rigidbody2D, PlayerStats);
            _playerCollisionHandler.Initialize(PlayerStats.Health, ExperienceStorage);
            _jumper.Initialize(PlayerStats);
            _magnet.Initialize(PlayerStats, transform);

            _healthRegenerator.Initialize(PlayerStats.Health, PlayerStats.HealthRegenerateAmount);
            _shieldRegenerator.Initialize(PlayerStats.ShieldAmount, PlayerStats.Health);

            _weaponHolder.Weapon.Initialize(PlayerStats);
            _injuredScreenView.Initialize(PlayerStats.Health);
        }

        public void TakeDamage(float amount)
        {
            if (_isDamaged == false)
                PlayerStats.Health.TakeDamage(amount);

            TakeImmortality(TakeDamageImmortalityTime);
        }

        public void TakeStun(float time)
        {
            _stunCoroutine ??= StartCoroutine(TakingStun(time));
        }
    
        public void Die()
        {
            OnDeath?.Invoke();
        }

        public void Revive(float immortalityTime)
        {
            EndCoroutine(ref _immortalityCoroutine);
            EndCoroutine(ref _stunCoroutine);
        
            PlayerStats.Health.Heal(PlayerStats.Health.MaxHealth);
            TakeImmortality(immortalityTime);
        
            _entityStateMachine.SwitchState<PlayerIdleState>();
        }
    
        private void TakeImmortality(float time)
        {
            _immortalityCoroutine ??= StartCoroutine(TakingImmortality(time));
        }

        private void EndCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
    
            coroutine = null;
        }

        private IEnumerator TakingStun(float time)
        {
            var waitForStunTime = new WaitForSeconds(time);
        
            _entityStateMachine.SwitchState<PlayerStunnedState>();

            yield return waitForStunTime;

            _entityStateMachine.SwitchState<PlayerIdleState>();
        
            EndCoroutine(ref _stunCoroutine);
        }

        private IEnumerator TakingImmortality(float time)
        {
            var waitForImmortalityTime = new WaitForSeconds(time);
        
            _isDamaged = true;
        
            yield return waitForImmortalityTime;
        
            _isDamaged = false;

            EndCoroutine(ref _immortalityCoroutine);
        }

        private void Shoot()
        {
            _weaponHolder.Shoot();
        }
    }
}