using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class IncrementalReloadWeapon : Weapon
{
    [SerializeField] private int _currentMagazineSize;

    private Coroutine _reloadCoroutine;

    private int _magazineSize => ((IIncrementalWeaponStats)_weaponStats).WeaponMagazineSize;
    private float _currentRecharchingTime => ((IIncrementalWeaponStats)_weaponStats).WeaponRechargingTime;

    public event Action<int, int> OnAmmoUpdated;

    public bool IsReloading { get; private set; }
    public int MagazineSize => _currentMagazineSize;

    public override void Initialize(IWeaponStats weaponStats)
    {
        base.Initialize(weaponStats);
        _currentMagazineSize = _magazineSize;

        OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
    }

    public override bool TryAttack()
    {
        if (_currentMagazineSize > 0 && _isReloaded)
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }

            IsReloading = false;

            _currentMagazineSize--;
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());

            Attack();

            OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
            _isReloaded = false;

            return true;
        }

        return false;
    }

    private IEnumerator ReloadCoroutine()
    {
        float reloadDelay = 0.5f;

        yield return new WaitForSeconds(reloadDelay);

        IsReloading = true;

        while (_currentMagazineSize < _magazineSize)
        {
            OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
 
            yield return new WaitForSeconds(_currentRecharchingTime);

            _currentMagazineSize++;

            if (_currentMagazineSize >= _magazineSize)
            {
                _reloadCoroutine = null;
                IsReloading = false;
                yield break;
            }
        }
    }
}