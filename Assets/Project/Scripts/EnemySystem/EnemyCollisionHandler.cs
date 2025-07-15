using Project.Scripts.Interfaces;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public class EnemyCollisionHandler : CollisionHandler
    {
        [SerializeField] private float _pushForce;
        [SerializeField] private float _collisionDamage;
    
        protected override void HandleCollision(Collider2D collider)
        {
            if (collider.TryGetComponent(out Player player) == false)
                return;

            if (player is IDamageable damagable)
            {
                damagable.TakeDamage(_collisionDamage);
            }
        }
    }
}