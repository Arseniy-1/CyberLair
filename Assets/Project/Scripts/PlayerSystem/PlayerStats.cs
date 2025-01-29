using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IJumpStats, IMoverStats, IIncrementalWeaponStats, IMagnetStats
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

    [field: SerializeField] public float MagnetRange { get; private set; }
    [field: SerializeField] public float MagnetForce { get; private set; }
    
    public void SetDamage(int amount)
    {
        if (amount < 0)
            return;

        WeaponDamage = amount;
    }

    public void SetSpeed(float amount)
    {
        if (amount < 0)
            return;
        
        Speed = amount;
    }
    
    public void SetJumpDistance(float amount)
    {
        if (amount < 0)
            return;

        JumpDistance = amount;
    }
    
    public void SetJumpRealoadTime(float amount)
    {
        if (amount < 0)
            return;

        JumpReloadTime = amount;
    }
    
    public void SetJumpTime(float amount)
    {
        if (amount < 0)
            return;

        JumpTime = amount;
    }
    
    public void SetWeaponDamage(int amount)
    {
        if (amount < 0)
            return;

        WeaponDamage = amount;
    }
    
    public void SetMagazineSize(int amount)
    {
        if (amount < 0)
            return;

        WeaponMagazineSize = amount;
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
    
    public void SetWeaponRechargeTime(float amount)
    {
        if (amount < 0)
            return;

        WeaponRechargingTime = amount;
    }

    public void SetMagnetRange(float amount)
    {
        if (amount < 0)
            return;
        
        MagnetRange = amount;
    }

    public void SetMagnetForce(float amount)
    {
        if (amount < 0)
            return;
        
        MagnetForce = amount;
    }

    public PlayerStats DeepCopy()
    {
        return new PlayerStats
        {
            Speed = Speed,
            Health = Health.Copy(),
            
            JumpDistance = JumpDistance,
            JumpTime = JumpTime,
            JumpReloadTime = JumpReloadTime,
            
            WeaponSpread = WeaponSpread,
            WeaponDamage = WeaponDamage,
            WeaponBulletReloadTime = WeaponBulletReloadTime,
            WeaponRechargingTime = WeaponRechargingTime,
            WeaponMagazineSize = WeaponMagazineSize
        };
    }
}