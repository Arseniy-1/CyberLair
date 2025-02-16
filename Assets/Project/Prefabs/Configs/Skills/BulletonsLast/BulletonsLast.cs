using System;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.BulletonsLast
{
    [Serializable]
    public class BulletonsLast
    {
        [SerializeField] private StatModifier _damageModifier;

        private IncrementalReloadWeapon _incrementalWeapon;

        public void Initialize(Weapon weapon)
        {
            _incrementalWeapon = weapon as IncrementalReloadWeapon;

            if (_incrementalWeapon == false)
                throw new ArgumentNullException($"{nameof(weapon)} должен быть {nameof(IncrementalReloadWeapon)}");

            _incrementalWeapon.OnAmmoUpdated += HandleAmmoUpdated;
        }

        private void HandleAmmoUpdated(int currentAmmoCount, int magazineSize)
        {
            if (currentAmmoCount == 1)
                _incrementalWeapon.WeaponStats.WeaponDamage.AddModifier(_damageModifier);
            
            if(currentAmmoCount != 1)
                _incrementalWeapon.WeaponStats.WeaponDamage.RemoveModifier(_damageModifier);
        }
    }
}