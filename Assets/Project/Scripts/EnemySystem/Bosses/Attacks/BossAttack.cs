using System;
using System.Collections;
using Project.Scripts.EnemySystem.AttackTypes;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class BossAttack : MonoBehaviour
    {
        [SerializeField] protected BaseEnemyAttackStats Stats;
        [SerializeField] protected AttackAnimationEvents AnimationEvents;
        
        public event Action AttackPerformed;
        
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }

        public abstract IEnumerator Attack();
        
        public abstract void Disable();

        protected void EndAttack()
        {
            AttackPerformed?.Invoke();
        }
    }
}