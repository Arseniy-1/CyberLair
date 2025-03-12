using System;
using System.Collections;
using Project.Scripts.EnemySystem.AttackTypes;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class BossAttack : MonoBehaviour
    {
        [SerializeField] protected AttackAnimationEvents AnimatorEvents;
        [SerializeField] protected Animator AttackAnimator;
        [SerializeField] protected SpriteRenderer View;
        
        protected readonly int AttackTrigger = Animator.StringToHash("Attack");
        
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public BaseEnemyAttackStats AttackStats { get; private set; }
        public int BossAttackAnimationTrigger { get; protected set; }

        public abstract void Initialize();
        
        protected abstract void Disable();
        
        public virtual IEnumerator Performing()
        {
            var waitRecovery = new WaitForSeconds(AttackStats.AttackRecovery);
            
            for (int i = 0; i < AttackStats.AttackCount; i++)
            {
                yield return Attack();
            }

            yield return waitRecovery;
        }
        
        protected abstract IEnumerator Attack();
    }
}