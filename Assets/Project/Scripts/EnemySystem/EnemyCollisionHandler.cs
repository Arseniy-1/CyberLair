using UnityEngine;

public class EnemyCollisionHandler : CollisionHandler
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _stunTime;
    
    private int _collisionDamage;

    public void Initialize(int collisionDamage)
    {
        _collisionDamage = collisionDamage;
    }
    
    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player) == false)
            return;
        
        if (player is IStunable stunable)
        {
            stunable.TakeStun(_stunTime);
            Vector3 pushDirection = (player.transform.position - transform.position).normalized;

            stunable.Rigidbody2D.AddForce(pushDirection * _pushForce, ForceMode2D.Force);
        }

        if (player is IDamageable damagable)
        {
            damagable.TakeDamage(_collisionDamage);
        }
    }
}