using System;
using UnityEngine;

[Serializable]
public class PlayerStats : IStats
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }

    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }

    public void Initialize(PlayerConfig playerConfig)
    {
        Damage = playerConfig.Damage;
        JumpDistance = playerConfig.JumpDistance;
        JumpTime = playerConfig.JumpTime;
        ReloadTime = playerConfig.ReloadTime;
        BulletPrefab = playerConfig.BulletPrefab;
    }
}

public class EnemyConfig : MonoBehaviour
{
    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }

    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
}

public class EnemyStats : IStats
{
    [field: SerializeField] public float Damage;
    [field: SerializeField] public Health Health;

    [field: SerializeField] public float JumpDistance; 
    [field: SerializeField] public float JumpTime;

    [field: SerializeField] public float ReloadTime;
    [field: SerializeField] public Bullet BulletPrefab;

    public void Initialize(PlayerConfig playerConfig)
    {
        Damage = playerConfig.Damage;
        JumpDistance = playerConfig.JumpDistance;
        JumpTime = playerConfig.JumpTime;
        ReloadTime = playerConfig.ReloadTime;
        BulletPrefab = playerConfig.BulletPrefab;
    }
}

public interface IStats
{
    
}