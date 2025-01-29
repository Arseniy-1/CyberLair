using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyStats : IMoverStats, IAttackerStats
{
    [field: SerializeField] public int Experience { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float AttackRecovery { get; private set; }
    [field: SerializeField] public int AttackCount {  get; private set; }
}