using System;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [Serializable]
    public class BaseEnemyAttackStats : IAttackerStats
    {
        [field: SerializeField] public float AttackDelay { get; private set; }
        [field: SerializeField] public float AttackRecovery { get; private set; }
        [field: SerializeField] public int AttackCount {  get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}