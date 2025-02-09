using System;
using UnityEngine;

[Serializable]
public class SummonStats :  IWeaponStats, ISummonMoveStats
{
    private float _speed;
    [field: SerializeField] public Speed Speed { get; private set; }
    [field: SerializeField] public float MoveRadius { get; private set; }
    [field: SerializeField] public float MoveDelay { get; private set; }
    [field: SerializeField] public WeaponSpread WeaponSpread { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public BulletPerShootCount BulletPerShootCount { get; private set; }
    [field: SerializeField] public WeaponBulletReloadTime WeaponBulletReloadTime { get; private set; }

    public void SetWeaponDamage(int amount)
    {
        if (amount < 0)
            return;

        // WeaponDamage = amount;
    }

    public void SetSpeed(int amount)
    {
        if (amount < 0)
            return;

        // Speed = amount;
    }

    
    public void SetWeaponSpread(float amount)
    {
        if (amount < 0)
            return;

        // WeaponSpread = amount;
    }

    public void SetWeaponRealoadTime(float amount)
    {
        if (amount < 0)
            return;

        // WeaponBulletReloadTime = amount;
    }
}