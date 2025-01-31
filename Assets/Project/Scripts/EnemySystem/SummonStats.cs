using System;
using UnityEngine;

[Serializable]
public class SummonStats :  IWeaponStats, ISummonMoveStats
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float MoveRadius { get; private set; }
    [field: SerializeField] public float MoveDelay { get; private set; }
    [field: SerializeField] public float WeaponSpread { get; private set; }
    [field: SerializeField] public int WeaponDamage { get; private set; }
    [field: SerializeField] public int BulletPerShootCount { get; private set; } = 1;
    [field: SerializeField] public float WeaponBulletReloadTime { get; private set; }

    public void SetWeaponDamage(int amount)
    {
        if (amount < 0)
            return;

        WeaponDamage = amount;
    }

    public void SetSpeed(int amount)
    {
        if (amount < 0)
            return;

        Speed = amount;
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
}