using System.Collections;
using Cysharp.Threading.Tasks.Triggers;
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
        protected bool IsAttacking;
        
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public BaseEnemyAttackStats AttackStats { get; private set; }
        [field: SerializeField] public float CoolDawn { get; private set; }
        public int BossAttackAnimationTrigger { get; protected set; }

        public abstract void Initialize();
        
        public abstract void Disable();
        
        public IEnumerator Performing()
        {
            var waitRecovery = new WaitForSeconds(AttackStats.AttackRecovery);
            IsAttacking = false;
            
            View.gameObject.SetActive(true);
            AnimatorEvents.Attacking += HandleAttacking;
            AttackAnimator.SetTrigger(AttackTrigger);
            
            yield return new WaitUntil(() => IsAttacking);
            
            for (int i = 0; i < AttackStats.AttackCount; i++)
            {
                yield return Attack();
            }

            yield return waitRecovery;
        }
        
        protected abstract IEnumerator Attack();

        private void HandleAttacking()
        {
            AnimatorEvents.Attacking -= HandleAttacking;
            AnimatorEvents.Ending += HandleEnding;
            
            IsAttacking = true;
        }

        private void HandleEnding()
        {
            AnimatorEvents.Ending -= HandleEnding;
            
            IsAttacking = false;
        }
    }
}