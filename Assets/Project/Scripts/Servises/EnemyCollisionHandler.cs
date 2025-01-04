using UnityEngine;

public class EnemyCollisionHandler : CollisionHandler
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _stunTime;
    [SerializeField] private int _collisionDamage;

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            if (player is IStunable stunable)
            {
                stunable.TakeStun(_stunTime);
                Vector3 pushDirection = (transform.position - player.transform.position).normalized;

                stunable.Rigidbody2D.AddForce(pushDirection * _pushForce, ForceMode2D.Force);
            }

            if (player is IDamagable damagable)
            {
                damagable.TakeDamage(_collisionDamage);
            }
        }
    }
}