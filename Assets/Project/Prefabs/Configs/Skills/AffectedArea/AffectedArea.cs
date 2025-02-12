using System;
using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.AffectedArea
{
    [Serializable]
    public class AffectedArea
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Range(0f, 1f)] private float _chance;

        public void Initialize(Weapon weapon)
        {
            weapon.OnShooted += InnerSubscribe;
        }

        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDamagableCollided += Blow;
        }

        private void Blow(IDamageable damageable)
        {
            if (Random.value > _chance)
                return;
            
            var enemy = damageable as Enemy;

            if (enemy == false)
                return;

            Collider2D[] results = { };
            Physics2D.OverlapCircleNonAlloc(enemy.Position, _radius, results, _layerMask);

            foreach (Collider2D affected in results)
            {
                if (affected.TryGetComponent(out Enemy affectedEnemy))
                {
                    affectedEnemy.TakeDamage(_damage);
                }
            }
        }
    }
}