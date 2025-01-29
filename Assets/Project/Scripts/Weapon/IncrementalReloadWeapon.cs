using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class IncrementalReloadWeapon : Weapon
{
    [FormerlySerializedAs("_currentMagazineSize")] [SerializeField] private int currentCurrentMagazineSize;

    private Coroutine _reloadCoroutine;

    private int _magazineSize => ((IIncrementalWeaponStats)_weaponStats).WeaponMagazineSize;
    private float _currentRecharchingTime => ((IIncrementalWeaponStats)_weaponStats).WeaponRechargingTime;

    public event Action<int, int> OnAmmoUpdated;

    public bool IsReloading { get; private set; }
    public int CurrentMagazineSize => currentCurrentMagazineSize;

    public override void Initialize(IWeaponStats weaponStats)
    {
        base.Initialize(weaponStats);
        currentCurrentMagazineSize = _magazineSize;

        OnAmmoUpdated?.Invoke(currentCurrentMagazineSize, _magazineSize);
    }

    public override bool TryAttack()
    {
        if (currentCurrentMagazineSize > 0 && _isReloaded)
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }

            IsReloading = false;

            currentCurrentMagazineSize--;
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());

            Attack();

            OnAmmoUpdated?.Invoke(currentCurrentMagazineSize, _magazineSize);
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

        while (currentCurrentMagazineSize < _magazineSize)
        {
            OnAmmoUpdated?.Invoke(currentCurrentMagazineSize, _magazineSize);
 
            yield return new WaitForSeconds(_currentRecharchingTime);

            currentCurrentMagazineSize++;

            if (currentCurrentMagazineSize >= _magazineSize)
            {
                _reloadCoroutine = null;
                IsReloading = false;
                yield break;
            }
        }
    }
}