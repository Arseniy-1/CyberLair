using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [Serializable]
    public class EnemyJumpStats : IJumpStats
    {
        [field: SerializeField] public JumpSpeed JumpSpeed { get; private set; }
        [field: SerializeField] public JumpTime JumpTime { get; private set; }
        [field: SerializeField] public JumpReloadTime JumpReloadTime { get; private set; }
    }
}