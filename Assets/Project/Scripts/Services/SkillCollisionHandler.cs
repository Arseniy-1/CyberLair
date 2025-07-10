using System;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.Servises
{
    public class SkillCollisionHandler : CollisionHandler
    {
        [SerializeField] private int _collisionDamage;
        [SerializeField] private int _contactLimit;
        
        private int _contactCount;
        
        public event Action ContactLimitExpired;

        private void OnEnable()
        {
            _contactCount = 0;
        }

        protected override void HandleCollision(Collider2D collider)
        {
            if (!collider.TryGetComponent(out IDamageable damagable)) return;
            
            damagable.TakeDamage(_collisionDamage);

            _contactCount++;
            HandleContactLimit();
        }
        
        private void HandleContactLimit()
        {
            if(_contactCount >= _contactLimit)
                ContactLimitExpired?.Invoke();
        }
    }
}