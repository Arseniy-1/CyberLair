using System;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.BulletonsLast
{
    public class BulletonsLast : ISkillInstance
    {
        private readonly IncrementalReloadWeapon _weapon;

        public BulletonsLast(SkillData skillData)
        {
            _weapon = skillData.WeaponHolder.Weapon as IncrementalReloadWeapon;
            
            if (_weapon)
                _weapon.Shooted += InnerSubscribe;
        }

        private void InnerSubscribe(Bullet bullet)
        {
            if (_weapon.CurrentMagazineSize != 0) return;
            
            bullet.OnDamagableCollided += DealCriticalDamage;
            bullet.OnDestroyed += Unsubscribe;
        }

        private void DealCriticalDamage(IDamageable damageable)
        {
            damageable.TakeDamage(_weapon.WeaponStats.WeaponDamage.CurrentValue);
        }

        private void Unsubscribe(Bullet bullet)
        {
            bullet.OnDamagableCollided -= DealCriticalDamage;
            bullet.OnDestroyed -= Unsubscribe;
        }

        public void Disable()
        {
            _weapon.Shooted -= InnerSubscribe;
        }
    }
}