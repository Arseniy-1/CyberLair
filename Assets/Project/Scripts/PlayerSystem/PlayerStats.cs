using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IMoverStats, IMagnetStats, IIncrementalWeaponStats, IJumpStats
{
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Speed Speed { get; private set; }
    [field: SerializeField] public JumpDistance JumpDistance { get; private set; }
    [field: SerializeField] public JumpTime JumpTime { get; private set; }
    [field: SerializeField] public JumpReloadTime JumpReloadTime { get; private set; }
    [field: SerializeField] public WeaponSpread WeaponSpread { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public BulletPerShootCount BulletPerShootCount { get; private set; }
    [field: SerializeField] public WeaponBulletReloadTime WeaponBulletReloadTime { get; private set; }
    [field: SerializeField] public WeaponRechargingTime WeaponRechargingTime { get; private set; }
    [field: SerializeField] public WeaponMagazineSize WeaponMagazineSize { get; private set; }
    [field: SerializeField] public MagnetRange MagnetRange { get; private set; }
    [field: SerializeField] public MagnetForce MagnetForce { get; private set; }

    public void Initialize()
    {
        Health.CalculateCurrentValue();
        Speed.CalculateCurrentValue();
        JumpDistance.CalculateCurrentValue();
        JumpTime.CalculateCurrentValue();
        JumpReloadTime.CalculateCurrentValue();
        WeaponSpread.CalculateCurrentValue();
        WeaponDamage.CalculateCurrentValue();
        BulletPerShootCount.CalculateCurrentValue();
        WeaponBulletReloadTime.CalculateCurrentValue();
        WeaponRechargingTime.CalculateCurrentValue();
        WeaponMagazineSize.CalculateCurrentValue();
        MagnetRange.CalculateCurrentValue();
        MagnetForce.CalculateCurrentValue();
    }
    
    public void Update()
    {
        Health.UpdateModifiers();
        Speed.UpdateModifiers();
        JumpDistance.UpdateModifiers();
        JumpTime.UpdateModifiers();
        JumpReloadTime.UpdateModifiers();
        WeaponSpread.UpdateModifiers();
        WeaponDamage.UpdateModifiers();
        BulletPerShootCount.UpdateModifiers();
        WeaponBulletReloadTime.UpdateModifiers();
        WeaponRechargingTime.UpdateModifiers();
        WeaponMagazineSize.UpdateModifiers();
        MagnetRange.UpdateModifiers();
        MagnetForce.UpdateModifiers();
    }
}