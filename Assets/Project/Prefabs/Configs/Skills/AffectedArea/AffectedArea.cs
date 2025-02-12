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
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Range(0f, 1f)] private float _damageProportion;
        [SerializeField, Range(0f, 1f)] private float _chance;

        private IWeaponStats _weaponStats;
        
        public void Initialize(Weapon weapon, IWeaponStats weaponStats)
        {
            weapon.OnShot += InnerSubscribe;

            _weaponStats = weaponStats;
        }

        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDestroyed += Blow;
        }

        private void Blow(Bullet bullet)
        {
            bullet.OnDestroyed -= Blow;
            
            if (Random.value > _chance)
                return;

            Collider2D[] results = Physics2D.OverlapCircleAll(bullet.transform.position, _radius, _layerMask);

            foreach (Collider2D affected in results)
            {
                if (affected.TryGetComponent(out Enemy affectedEnemy))
                {
                    affectedEnemy.TakeDamage(_weaponStats.WeaponDamage.CurrentValue * _damageProportion);
                }
            }
        }
    }
}