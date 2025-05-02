using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using StateMashineSytem;
using StateMashineSytem.PlayerStateMashine;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Unity.VisualScripting;
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
    [SerializeField] private CameraShakeSettings _cameraShakeSettings;
    [SerializeField] private InjuredScreenView _injuredScreenView;
    
    [SerializeField] private SoundPlayer _damageSoundPlayer;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private HealthRegenerator _healthRegenerator;
    [SerializeField] private ShieldRegenerator _shieldRegenerator;

    [SerializeField] private Collider2D _collider;
    
    private EntityStateMachine _entityStateMachine;
    private ExperienceStorage _experienceStorage = new ExperienceStorage();

    public event Action OnDeath;
    
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public Collider2D Collider2D => _collider;
    public ExperienceStorage ExperienceStorage => _experienceStorage;

    public Vector2 Position => transform.position;

    private void Awake()
    {
        InitializeComponents();
    }

    private void OnEnable()
    {
        _playerInputController.OnShootButtonPressed += Shoot;
    }

    private void OnDisable()
    {
        _playerInputController.OnShootButtonPressed -= Shoot;
    }

    private void Update()
    {
        PlayerStats.Update();
        _entityStateMachine.Update();
    }

    private void InitializeComponents()
    {
        List<IState> playerStates = new List<IState>
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
        _playerCollisionHandler.Initialize(PlayerStats.Health, _experienceStorage);
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
        _damageSoundPlayer.Play();
        MessageBrokerHolder.Camera.Publish(new M_CameraShake(_cameraShakeSettings));
        PlayerStats.Health.TakeDamage(amount);
        
        float imortalityTime = 1.5f;
        StartCoroutine(TakingImortality(imortalityTime));
    }

    public void TakeStun(float time)
    {
        StartCoroutine(TakingStun(time));
    }

    private IEnumerator TakingStun(float time)
    {
        _entityStateMachine.SwitchState<PlayerStunnedState>();
        
        yield return new WaitForSeconds(time);
        
        _entityStateMachine.SwitchState<PlayerIdleState>();
    }
    
    private IEnumerator TakingImortality(float time)
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(time);
        _collider.enabled = true;
    }

    private void Shoot()
    {
        _weaponHolder.Shoot();
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }
}