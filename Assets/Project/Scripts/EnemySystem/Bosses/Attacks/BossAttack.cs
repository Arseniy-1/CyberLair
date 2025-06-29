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

        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        protected bool IsAttacking;
        private EnemyTargetProvider _provider;
        
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public BaseEnemyAttackStats AttackStats { get; private set; }
        public int BossAttackAnimationTrigger { get; protected set; }

        public abstract void Initialize();
        
        public abstract void Disable();
        
        public IEnumerator Performing()
        {
            var waitRecovery = new WaitForSeconds(AttackStats.AttackRecovery);
            var waitForAttack = new WaitUntil(() => IsAttacking);
            
            View.gameObject.SetActive(true);
            AnimatorEvents.Attacking += HandleAttacking;
            AttackAnimator.SetTrigger(_attackTrigger);
            
            yield return waitForAttack;
            
            for (int i = 0; i < AttackStats.AttackCount; i++)
            {
                yield return Attack();
            }

            yield return waitRecovery;
            
            HandleEnding();
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