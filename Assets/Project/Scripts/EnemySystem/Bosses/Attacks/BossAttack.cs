using System;
using System.Collections;
using Project.Scripts.EnemySystem.AttackTypes;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class BossAttack : MonoBehaviour
    {
        [SerializeField] protected AttackAnimationEvents View;
        [SerializeField] protected Animator Animator;
        
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public BaseEnemyAttackStats AttackStats { get; private set; }
        
        protected abstract void Disable();
        
        public IEnumerator Performing()
        {
            var waitDelay = new WaitForSeconds(AttackStats.AttackDelay);
            var waitRecovery = new WaitForSeconds(AttackStats.AttackRecovery);
            
            for (int i = 0; i < AttackStats.AttackCount; i++)
            {
                yield return waitDelay;
                yield return Attack();
            }

            yield return waitRecovery;
            
            Disable();
        }
        
        protected abstract IEnumerator Attack();
    }
}