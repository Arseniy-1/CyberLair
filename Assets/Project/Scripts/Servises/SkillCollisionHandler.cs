using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.Servises
{
    public class SkillCollisionHandler : CollisionHandler
    {
        [SerializeField] private int _collisionDamage;
        [SerializeField, Tooltip("0 for infinity")] private int _contactLimit;
        
        private int _contactCount;
        
        public event Action ContactLimitExpired;

        private void OnEnable()
        {
            _contactCount = 0;
        }
        
        public void ApplyStats(int damage)
        {
            _collisionDamage = damage;
        }

        protected override void HandleCollision(Collider2D collider)
        {
            if (!collider.TryGetComponent(out Enemy enemy)) return;

            if (enemy is not IDamageable damagable) return;
            
            damagable.TakeDamage(_collisionDamage);

            _contactCount++;
            HandleContactLimit();
        }
        
        private void HandleContactLimit()
        {
            if(_contactCount == _contactLimit)
                ContactLimitExpired?.Invoke();
        }
    }
}