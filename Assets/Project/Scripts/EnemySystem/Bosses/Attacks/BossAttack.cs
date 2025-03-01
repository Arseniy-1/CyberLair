using System;
using Project.Scripts.EnemySystem.AttackTypes;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class BossAttack : MonoBehaviour
    {
        [SerializeField] protected BaseEnemyAttackStats Stats;
        
        public event Action AttackPerformed;
        
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }

        public abstract void Attack();
        
        public abstract void Disable();
    }
}