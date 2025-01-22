using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IJumpStats
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    
    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }
    
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }

    public void SetDamage(int amount)
    {
        Damage = amount;
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

[Serializable]
public class EnemyStats : IJumpStats
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }

    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
}

public interface IJumpStats
{
    float JumpDistance { get; }
    float JumpTime { get; }
}