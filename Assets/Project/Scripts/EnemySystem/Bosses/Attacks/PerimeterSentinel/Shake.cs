using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.Attacks.PerimeterSentinel
{
    public class Shake : MonoBehaviour, IDestoyable<Shake>, IReturnable
    {
        private readonly int _shake = Animator.StringToHash("Shake");
        
        [SerializeField] private AttackAnimationEvents _animationEvents;
        [SerializeField] private Animator _animator;
        
        private int _damage;
        private List<IDamageable> _collides;
        
        public event Action<Shake> OnDestroyed;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                _collides.Add(damageable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                _collides.Remove(damageable);
        }
        
        private void OnDisable()
        {
            ReturnToPool();
        }
        
        public void Initialize(int damage)
        {
            _collides = new List<IDamageable>();
            
            _damage = damage;

            _animationEvents.Attacking += DealDamage;
            _animationEvents.Ending += ReturnToPool;
            
            _animator.SetTrigger(_shake);
        }

        public void ReturnToPool()
        {
            _animationEvents.Attacking -= DealDamage;
            _animationEvents.Ending -= ReturnToPool;
            
            OnDestroyed?.Invoke(this);
        }

        private void DealDamage()
        {
            _collides.ToList().ForEach(damageable => damageable.TakeDamage(_damage));
        }
    }
}