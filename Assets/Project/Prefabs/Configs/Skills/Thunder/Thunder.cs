using System;
using System.Collections;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Thunder : MonoBehaviour
    {
        [SerializeField] private float _actionRadius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _damage;
        [SerializeField] private float _shootsNeeded;

        private float _shootsPassed;
        private Weapon _weapon;
        
        private Vector2 TargetPosition => transform.position;

        private void OnDisable()
        {
            _weapon.OnShot -= HandleShoot;
        }

        public void Initialize(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.OnShot += HandleShoot;
        }

        private void HandleShoot(Bullet bullet)
        {
            if (_shootsPassed >= _shootsNeeded)
            {
                _shootsPassed = 0;
                Strike();

                return;
            }

            _shootsPassed++;
        }
        
        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, _actionRadius, _layerMask);
            
            if(colliders.Length == 0)
                return;
            
            Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

            if (strickenCollider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
}