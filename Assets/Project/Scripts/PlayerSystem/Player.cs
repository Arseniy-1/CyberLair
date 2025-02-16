using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using StateMashineSytem;
using StateMashineSytem.PlayerStateMashine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour, ITarget, IDamageable, IStunable, IDieable
{
    [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ExperienceStorage _experienceStorage;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private TargetScanner _targetScanner;
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private Magnet _magnet;
    [SerializeField] private HealthRegenerator _healthRegenerator;
    
    private Collider2D _collider;
    private EntityStateMachine _entityStateMachine;

    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public bool IsStunned { get; private set; } = false;

    public Vector2 Position => transform.position;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
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
            new PlayerIdleState(this, _playerMover, _rigidbody2D, _weaponHolder, _targetScanner),
            new PlayerMoveState(this, _playerInputController, _playerMover, _weaponHolder, _targetScanner, _jumper),
            new PlayerJumpState(_playerInputController, _collider, _jumper),
            new PlayerStunnedState(this, _playerMover)
        };

        _entityStateMachine = new EntityStateMachine(playerStates);

        foreach (IState state in playerStates)
        {
            state.Initialize(_entityStateMachine);
        }

        PlayerStats.Initialize();
        _destroyer.Initialize(PlayerStats.Health, this);
        _playerMover.Initialize(_playerInputController, _rigidbody2D, PlayerStats);
        _playerCollisionHandler.Initialize(PlayerStats.Health, _experienceStorage);
        _jumper.Initialize(PlayerStats);
        _magnet.Initialize(PlayerStats, transform);
        _healthRegenerator.Initialize(PlayerStats.Health, PlayerStats.RegenerateAmount);
        
        _weaponHolder.Weapon.Initialize(PlayerStats);
    }

    public void TakeDamage(float amount)
    {
        PlayerStats.Health.TakeDamage(amount);
    }

    public void TakeStun(float time)
    {
        StartCoroutine(TakingStun(time));
    }

    private IEnumerator TakingStun(float time)
    {
        IsStunned = true;
        yield return new WaitForSeconds(time);

        IsStunned = false;
    }

    private void Shoot()
    {
        _weaponHolder.Shoot();
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}