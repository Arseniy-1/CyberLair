using System;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StunZap
{
    [Serializable]
    public class StunZap
    {
        [SerializeField] private float _stunDuration;
        
        public void Initialize(Weapon weapon)
        {
            weapon.OnShooted += InnerSubscribe;
        }
        
        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDamagableCollided += StunEnemy;
        }

        private void StunEnemy(IDamageable damageable)
        {
            (damageable as IStunable)?.TakeStun(_stunDuration);
        }
    }
}