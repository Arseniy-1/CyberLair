using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IJumpStats, IMoverStats, IIncrementalWeaponStats
{
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }
    [field: SerializeField] public float JumpReloadTime { get; private set; }
    [field: SerializeField] public float WeaponSpread { get; private set; }

    [field: SerializeField] public int WeaponDamage { get; private set; }
    [field: SerializeField] public float WeaponBulletReloadTime { get; private set; }
    [field: SerializeField] public float WeaponRechargingTime { get; private set; }
    [field: SerializeField] public int WeaponMagazineSize { get; private set; }

    public void SetDamage(int amount)
    {
        if (amount < 0)
            return;

        WeaponDamage = amount;
    }

    public void SetJumpDistance(float amount)
    {
        if (amount < 0)
            return;

        JumpDistance = amount;
    }
    
    public void SetWeaponDamage(int amount)
    {
        if (amount < 0)
            return;

        WeaponDamage = amount;
    }
    
    public void SetWeaponSpread(float amount)
    {
        if (amount < 0)
            return;

        WeaponSpread = amount;
    }
    
    public void SetWeaponRealoadTime(float amount)
    {
        if (amount < 0)
            return;

        WeaponBulletReloadTime = amount;
    }

    public PlayerStats DeepCopy()
    {
        return new PlayerStats
        {
            WeaponDamage = WeaponDamage,
            Health = Health.Copy(),
            JumpDistance = JumpDistance,
            JumpTime = JumpTime,
            WeaponBulletReloadTime = WeaponBulletReloadTime,
        };
    }
}