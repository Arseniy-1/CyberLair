using System;
using UnityEngine;

[Serializable]
public class EnemyStats : IJumpStats, IMoverStats
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }
    [field: SerializeField] public float JumpReloadTime { get; private set; } = 0;

    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
}