using System;
using System.Collections;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class IncrementalReloadWeapon : Weapon
    {
        [SerializeField] private int _currentMagazineSize;
        [SerializeField] private float _reloadDelay = 1f;
    
        [SerializeField] private ParticleSystem _shootParticles;
        [SerializeField] private AudioID _reloadSound = AudioID.Reload;
        [SerializeField] private AudioID _fullReloadSound = AudioID.FullReload;
        [SerializeField] private AudioID _outOfAmmoSound = AudioID.OutOfAmmo;
    
        private Coroutine _reloadCoroutine;
        private WaitForSeconds _waitForDelay;
        private WaitForSeconds _waitForRechargingTime;

        private int MagazineSize => (int)((IIncrementalWeaponStats)_weaponStats).WeaponMagazineSize.CurrentValue;
        private float CurrentRechargingTime => ((IIncrementalWeaponStats)_weaponStats).WeaponRechargingTime.CurrentValue;
        public int CurrentMagazineSize => _currentMagazineSize;

        public bool IsReloading { get; private set; }
    
        public event Action<int, int> OnAmmoUpdated;

        public override void Initialize(IWeaponStats weaponStats)
        {
            base.Initialize(weaponStats);
            _currentMagazineSize = MagazineSize;
        
            _waitForDelay ??= new WaitForSeconds(_reloadDelay);
            _waitForRechargingTime ??= new WaitForSeconds(CurrentRechargingTime);

            OnAmmoUpdated?.Invoke(_currentMagazineSize, MagazineSize);
        }

        public override bool TryAttack()
        {
            if (_currentMagazineSize <= 0 || !IsReloaded) 
                return false;
        
            if (_reloadCoroutine != null)
                StopCoroutine(_reloadCoroutine);

            IsReloading = false;

            _currentMagazineSize--;
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());
            _shootParticles.Play();
        
            Attack();

            if (_currentMagazineSize == 0)
                _outOfAmmoSound.Play();
        
            OnAmmoUpdated?.Invoke(_currentMagazineSize, MagazineSize);
        
            IsReloaded = false;

            return true;

        }

        private IEnumerator ReloadCoroutine()
        {
            yield return _waitForDelay;

            IsReloading = true;

            while (_currentMagazineSize < MagazineSize)
            {
                OnAmmoUpdated?.Invoke(_currentMagazineSize, MagazineSize);
 
                yield return _waitForRechargingTime;

                _currentMagazineSize++;
                _reloadSound.Play();

                if (_currentMagazineSize < MagazineSize)
                    continue;
            
                _reloadCoroutine = null;
                IsReloading = false;
                _fullReloadSound.Play();
            
                yield break;
            }
        }
    }
}