using UnityEngine;

public class Player : MonoBehaviour, ITarget, IDamagable
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Health _health;

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

    public void InitializeComponents()
    {
        _playerMover.Initialize(_playerInputController, _rigidbody2D);
    }

    public void TakeDamage(int amount)
    {
        _health.TakeDamage(amount);
    }

    public void Heal(int amount)
    {
        _health.Heal(amount);
    }

    private void Shoot()
    {
        _weaponHolder.Shoot();
    }
}