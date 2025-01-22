using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IJumpStats, IMoverStats
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }
    [field: SerializeField] public float JumpReloadTime { get; private set; }

    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }

    public void SetDamage(int amount)
    {
        if(amount < 0)
            return;
        
        Damage = amount;
    }
    
    public void SetJumpDistance(float amount)
    {
        if(amount < 0)
            return;

        JumpDistance = amount;
    }

    public PlayerStats DeepCopy()
    {
        return new PlayerStats
        {
            Damage = this.Damage,
            Health = this.Health.Copy(),
            JumpDistance = this.JumpDistance,
            JumpTime = this.JumpTime,
            ReloadTime = this.ReloadTime,
            BulletPrefab = this.BulletPrefab.Copy()
        };
    }
}

public interface IJumpStats
{
    float JumpDistance { get; }
    float JumpTime { get; }
    float JumpReloadTime { get; }
}

public interface IMoverStats
{
    float Speed { get; }
}