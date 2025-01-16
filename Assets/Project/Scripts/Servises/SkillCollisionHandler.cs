using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.Servises
{
    public class SkillCollisionHandler : CollisionHandler
    {
        [SerializeField] private int _collisionDamage;

        protected override void HandleCollision(Collider2D collider)
        {
            if (!collider.TryGetComponent(out Enemy enemy)) return;
            
            if (enemy is IDamagable damagable)
            {
                damagable.TakeDamage(_collisionDamage);
            }
        }
    }
}