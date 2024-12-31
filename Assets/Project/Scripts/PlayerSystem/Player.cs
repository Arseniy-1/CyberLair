using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMashineSytem;
using StateMashineSytem.PlayerStateMashine;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour, ITarget, IDamagable, IStunable
{
    [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Health _health;
    [SerializeField] private ExperienceStorage _experienceStorage;
    [SerializeField] private Jumper _jumper;

    private Collider2D _collider;
    private EntityStateMachine _entityStateMachine;

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
        _entityStateMachine.Update();
    }

    public void InitializeComponents()
    {
        List<IState> playerStates = new List<IState>
        {
            new PlayerIdleState(this, _playerMover, _rigidbody2D),
            new PlayerMoveState(this,_playerInputController, _playerMover),
            new PlayerJumpState(this, _playerInputController, _collider, _jumper),
            new PlayerStunnedState(this, _playerMover)
        };

        _entityStateMachine = new EntityStateMachine(playerStates);

        foreach (IState state in playerStates)
        {
            state.Initialize(_entityStateMachine);
        }

        _playerMover.Initialize(_playerInputController, _rigidbody2D);
        _playerCollisionHandler.Initialize(_health, _experienceStorage);
    }

    public void TakeDamage(int amount)
    {
        _health.TakeDamage(amount);
    }

    public void TakeStun(float time)
    {
        StartCoroutine(TakingStun(time));
    }

    public void Heal(int amount)
    {
        _health.Heal(amount);
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
}
