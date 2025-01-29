using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [Serializable]
    public class EnemyJumpStats : IJumpStats
    {
        [field: SerializeField] public float JumpDistance { get; private set; }
        [field: SerializeField] public float JumpTime { get; private set; }
        [field: SerializeField] public float JumpReloadTime { get; private set; }
    }
}