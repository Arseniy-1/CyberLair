using System;
using System.Collections;
using Project.Scripts.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

public class IncrementalReloadWeapon : Weapon
{
    [SerializeField] private int _currentMagazineSize;
    
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private SoundPlayer _reloadSoundPlayer;
    [SerializeField] private SoundPlayer _fullReloadSoundPlayer;
    [SerializeField] private SoundPlayer _outOfAmmoSoundPlayer;
    
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
        if (_currentMagazineSize > 0 && IsReloaded)
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }

            IsReloading = false;

            _currentMagazineSize--;
            _reloadCoroutine = StartCoroutine(ReloadCoroutine());

            _shootParticles.Play();
            Attack();

        if (_currentMagazineSize == 0)
            _outOfAmmoSoundPlayer.Play();
        
            OnAmmoUpdated?.Invoke(_currentMagazineSize, _magazineSize);
            IsReloaded = false;

            return true;
        }

        return false;
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
            _reloadSoundPlayer.Play();
            
            if (_currentMagazineSize >= _magazineSize)
            {
                _reloadCoroutine = null;
                IsReloading = false;
                _fullReloadSoundPlayer.Play();
                yield break;
            }
        }
    }
}