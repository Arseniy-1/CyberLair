using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using StateMashineSytem;
using StateMashineSytem.PlayerStateMashine;
using IState = StateMashineSytem.IState;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour, ITarget, IDamageable, IStunable, IDieable
{
    [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private PlayerInputController _playerInputController;
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
    public event Action OnTakeDamage;

    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public Vector2 Position => transform.position;
    
    public ExperienceStorage ExperienceStorage { get; } = new();

    private void Awake()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        _playerInputController.OnShootButtonPressed += Shoot;
    }

    private void Update()
    {
        PlayerStats.Update();
        _entityStateMachine.Update();
    }
    
    private void OnDisable()
    {
        _playerInputController.OnShootButtonPressed -= Shoot;
    }

    private void InitializeComponents()
    {
        var playerStates = new List<IState>
        {
            new PlayerIdleState(_playerMover, _rigidbody2D, _weaponHolder, _targetScanner),
            new PlayerMoveState(_playerInputController, _playerMover, _weaponHolder, _targetScanner, _jumper),
            new PlayerJumpState(_playerInputController, _collider, _jumper),
            new PlayerStunnedState(_playerMover, _jumper)
        };

        _entityStateMachine = new EntityStateMachine(playerStates);

        foreach (IState state in playerStates)
        {
            state.Initialize(_entityStateMachine, _animator);
        }

        _entityStateMachine.Initialize();
        PlayerStats.Initialize();
        _destroyer.Initialize(PlayerStats.Health, this);
        _playerMover.Initialize(_playerInputController, _rigidbody2D, PlayerStats);
        _playerCollisionHandler.Initialize(PlayerStats.Health, ExperienceStorage);
        _jumper.Initialize(PlayerStats);
        _magnet.Initialize(PlayerStats, transform);

        _healthRegenerator.Initialize(PlayerStats.Health, PlayerStats.HealthRegenerateAmount);
        _shieldRegenerator.Initialize(PlayerStats.ShieldAmount, PlayerStats.Health);

        _weaponHolder.Weapon.Initialize(PlayerStats);
        _injuredScreenView.Initialize(PlayerStats.Health);
    }

    [Button]
    public void TakeDamage(float amount)
    {
        if (_isDamaged == false)
            PlayerStats.Health.TakeDamage(amount);

        float immortalityTime = 0.7f;

        TakeImmortality(immortalityTime);
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
        if(coroutine != null)
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
        OnTakeDamage?.Invoke();
        
        yield return waitForImmortalityTime;
        
        _isDamaged = false;

        EndCoroutine(ref _immortalityCoroutine);
    }

    private void Shoot()
    {
        _weaponHolder.Shoot();
    }
}