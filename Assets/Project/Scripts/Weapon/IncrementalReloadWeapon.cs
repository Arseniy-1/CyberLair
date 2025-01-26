using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon
{
    public class IncrementalReloadWeapon : Weapon
    {
        [SerializeField] private int _currentAmmo;
        private Coroutine _reloadCoroutine;
        
        private IIncrementalWeaponStats _weaponStats;
        
        public bool IsRealoading = true; 

        
        public event Action<int, int> OnAmmoUpdated;

        private void Start()
        {
            _currentAmmo = _weaponStats.WeaponMagazineSize; 
            OnAmmoUpdated?.Invoke(_currentAmmo, _weaponStats.WeaponMagazineSize);
        }

        public override bool TryAttack()
        {
            if (_currentAmmo > 0)
            {
                if (base.TryAttack())
                {
                    if (_reloadCoroutine != null)
                    {
                        StopCoroutine(_reloadCoroutine);
                    }
                    
                    _currentAmmo--;
                    _reloadCoroutine = StartCoroutine(ReloadCoroutine());
                    
                    return true;
                }
            }
            
            return false;
        }

        private IEnumerator ReloadCoroutine()
        {
            IsRealoading = true;
            
            while (_currentAmmo < _weaponStats.WeaponMagazineSize)
            {
                yield return new WaitForSeconds(_weaponStats.WeaponRechargingTime);

                _currentAmmo++;
                OnAmmoUpdated?.Invoke(_currentAmmo, _weaponStats.WeaponMagazineSize);
                
                if (_currentAmmo >= _weaponStats.WeaponMagazineSize)
                {
                    _reloadCoroutine = null;
                    yield break;
                }
            }
            
            IsRealoading = false;
        }
    }
}