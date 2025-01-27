using Project.Scripts.Weapon;
using UnityEngine;

public class Summon : MonoBehaviour
{
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private SummonMover _mover;
    [SerializeField] private SummonStats _summonStats;
    [SerializeField] private TargetScanner _targetScanner;
    
    private int _nominalDamage;
    private float _nominalSpread;
    private float _nominalReloadTime;
    private float _nominalSpeed;

    private void FixedUpdate()
    {
        ITarget target = _targetScanner.ClosestTarget;

        if (target != null)
        {
            _weaponHolder.SpotTarget(target);
            _weaponHolder.Shoot();
        }

        _mover.MoveToNextPosition();
    }

    public void Initialize(Transform targetTransform)
    {
        _mover.Initialize(targetTransform, _summonStats);

        _nominalDamage = _summonStats.WeaponDamage;
        _nominalSpread = _summonStats.WeaponSpread;
        _nominalReloadTime = _summonStats.WeaponBulletReloadTime;
        _nominalSpeed = _summonStats.Speed;
        
        _weaponHolder.Weapon.Initialize(_summonStats);
    }

    public void ApplyStats(float speedMultiplier, float damageMultiplier, float reloadTimeMultiplier,
        float spreadMultiplier)
    {
        _summonStats.SetWeaponDamage((int)(_nominalDamage * damageMultiplier));
       _summonStats.SetWeaponSpread(_nominalSpread * spreadMultiplier);
       _summonStats.SetWeaponRealoadTime(_nominalReloadTime * reloadTimeMultiplier);
        _summonStats.SetSpeed((int)(_nominalSpeed * speedMultiplier));
    }

    public void ApplyWeapon(Weapon weapon)
    {
        var currentWeapon = Instantiate(weapon, _weaponHolder.transform);
        _weaponHolder.EquipWeapon(currentWeapon);
    }
}