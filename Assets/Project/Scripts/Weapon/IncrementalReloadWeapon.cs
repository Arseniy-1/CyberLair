using System;
using System.Collections;
using Project.Scripts.Weapon;
using UnityEngine;

public class IncrementalReloadWeapon : Weapon
{
    [SerializeField] private int currentMagazineSize;

    private Coroutine _reloadCoroutine;

    private int _magazineSize => (int)((IIncrementalWeaponStats)_weaponStats).WeaponMagazineSize.CurrentValue;
    private float _currentRecharchingTime => ((IIncrementalWeaponStats)_weaponStats).WeaponRechargingTime.CurrentValue;

    public event Action<int, int> OnAmmoUpdated;

    public bool IsReloading { get; private set; }
    public int CurrentMagazineSize => currentMagazineSize;

    public override void Initialize(IWeaponStats weaponStats)
    {
        base.Initialize(weaponStats);
        currentMagazineSize = _magazineSize;

        OnAmmoUpdated?.Invoke(currentMagazineSize, _magazineSize);
    }

    public override bool TryAttack()
    {
        if (currentMagazineSize > 0 && _isReloaded)
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }

            IsReloading = false;

            currentMagazineSize--;
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());

            Attack();

            OnAmmoUpdated?.Invoke(currentMagazineSize, _magazineSize);
            _isReloaded = false;

            return true;
        }

        return false;
    }

    private IEnumerator ReloadCoroutine()
    {
        float reloadDelay = 1f;

        yield return new WaitForSeconds(reloadDelay);

        IsReloading = true;

        while (currentMagazineSize < _magazineSize)
        {
            OnAmmoUpdated?.Invoke(currentMagazineSize, _magazineSize);
 
            yield return new WaitForSeconds(_currentRecharchingTime);

            currentMagazineSize++;

            if (currentMagazineSize >= _magazineSize)
            {
                _reloadCoroutine = null;
                IsReloading = false;
                yield break;
            }
        }
    }
}