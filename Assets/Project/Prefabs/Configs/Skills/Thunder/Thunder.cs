using System;
using System.Collections;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon.ActiveSkills
{
    [Serializable]
    public class Thunder
    {
        [SerializeField] private float _actionRadius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _damage;
        [SerializeField] private float _strikesCount;
        [SerializeField] private float _shootsNeeded;

        private float _shootsPassed;
        private Transform _target;
        private Weapon _weapon;
        
        private Vector2 TargetPosition => _target.position;

        public void Initialize(Weapon weapon, Transform target)
        {
            _weapon = weapon;
            _target = target;
            _weapon.Shooted += HandleShoot;
        }
        
        public void Disable()
        {
            _weapon.Shooted -= HandleShoot;
        }

        private void HandleShoot(Bullet bullet)
        {
            _shootsPassed++;

            if (_shootsPassed < _shootsNeeded) return;
            
            _shootsPassed = 0;
            Strike();
        }
        
        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, _actionRadius, _layerMask);

            for (int i = 0; i < _strikesCount; i++)
            {
                if(colliders.Length == 0)
                    return;
            
                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

                if (strickenCollider.TryGetComponent(out IDamageable affected))
                {
                    affected.TakeDamage(_damage);
                }
            }
        }
    }
}