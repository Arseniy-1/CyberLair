using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon
{
    public class IncrementalReloadWeapon : Weapon
    {
        [SerializeField, Range(1, 10)] private int _magazineSize = 5; // Размер магазина
        [SerializeField, Range(0.1f, 5f)] private float _bulletReloadTime = 1f; // Время на дозарядку одного патрона
        [SerializeField, Range(0.1f, 2f)] private float _shotCooldown = 0.5f; // Задержка между выстрелами

        [SerializeField] private int _currentAmmo;
        private Coroutine _reloadCoroutine;
        public bool IsRealoading = true; // Контроль задержки между выстрелами

        public event Action<int, int> OnAmmoUpdated; // Событие для интерфейса (текущие/максимум)

        private void Start()
        {
            _currentAmmo = _magazineSize; // Изначально магазин полон
            OnAmmoUpdated?.Invoke(_currentAmmo, _magazineSize);
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
            
            while (_currentAmmo < _magazineSize)
            {
                yield return new WaitForSeconds(_bulletReloadTime);

                _currentAmmo++;
                OnAmmoUpdated?.Invoke(_currentAmmo, _magazineSize);
                
                if (_currentAmmo >= _magazineSize)
                {
                    _reloadCoroutine = null;
                    yield break;
                }
            }
            
            IsRealoading = false;
        }
    }
}