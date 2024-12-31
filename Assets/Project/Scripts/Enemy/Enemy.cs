using UnityEngine;

public class Enemy : MonoBehaviour, ITarget, IDamagable
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Health _health;

    public Vector2 Position => transform.position;

    public void TakeDamage(int amount)
    {
        _health.TakeDamage(amount);
    }
}
