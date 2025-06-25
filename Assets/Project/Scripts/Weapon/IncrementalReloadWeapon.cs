using System;
using System.Collections;
using Project.Scripts.Weapon;
using UnityEngine;

public class IncrementalReloadWeapon : Weapon
{
    [SerializeField] private int _currentMagazineSize;
    
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private AudioID _reloadSound = AudioID.Reload;
    [SerializeField] private AudioID _fullReloadSound = AudioID.FullReload;
    [SerializeField] private AudioID _outOfAmmoSound = AudioID.OutOfAmmo;
    
    private Coroutine _reloadCoroutine;

    private int _magazineSize => (int)((IIncrementalWeaponStats)_weaponStats).WeaponMagazineSize.CurrentValue;
    private float _currentRecharchingTime => ((IIncrementalWeaponStats)_weaponStats).WeaponRechargingTime.CurrentValue;

    public event Action<int, int> OnAmmoUpdated;

    public bool IsReloading { get; private set; }
    public int CurrentMagazineSize => _currentMagazineSize;

    public override void Initialize(IWeaponStats weaponStats)
    {
        base.Initialize(weaponStats);
        _currentMagazineSize = _magazineSize;

        OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
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
        
        OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
        
        IsReloaded = false;

        return true;

    }

    private IEnumerator ReloadCoroutine()
    {
        float reloadDelay = 1f;

        yield return new WaitForSeconds(reloadDelay);

        IsReloading = true;

        while (_currentMagazineSize < _magazineSize)
        {
            OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
 
            yield return new WaitForSeconds(_currentRecharchingTime);

            _currentMagazineSize++;
            _reloadSound.Play();

            if (_currentMagazineSize < _magazineSize)
                continue;
            
            _reloadCoroutine = null;
            IsReloading = false;
            _fullReloadSound.Play();
            yield break;
        }
    }
}