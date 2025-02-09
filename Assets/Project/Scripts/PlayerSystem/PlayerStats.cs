using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IMoverStats, IMagnetStats, IIncrementalWeaponStats
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

    public void Update()
    {
        float deltaTime = Time.deltaTime;
        Debug.Log(deltaTime);
        Health.UpdateModifiers(deltaTime);
        Speed.UpdateModifiers(deltaTime);
        JumpDistance.UpdateModifiers(deltaTime);
        JumpTime.UpdateModifiers(deltaTime);
        JumpReloadTime.UpdateModifiers(deltaTime);
        WeaponSpread.UpdateModifiers(deltaTime);
        WeaponDamage.UpdateModifiers(deltaTime);
        BulletPerShootCount.UpdateModifiers(deltaTime);
        WeaponBulletReloadTime.UpdateModifiers(deltaTime);
        WeaponRechargingTime.UpdateModifiers(deltaTime);
        WeaponMagazineSize.UpdateModifiers(deltaTime);
        MagnetRange.UpdateModifiers(deltaTime);
        MagnetForce.UpdateModifiers(deltaTime);
    }
}