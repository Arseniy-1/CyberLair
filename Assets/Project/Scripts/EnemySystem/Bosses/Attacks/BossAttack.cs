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
        public int BossAttackAnimationTrigger { get; protected set; }

        public abstract void Initialize();
        
        public abstract void Disable();
        
        public IEnumerator Performing()
        {
            var waitRecovery = new WaitForSeconds(AttackStats.AttackRecovery);
            
            Debug.Log($"{gameObject.name} waiting IsAttacking = {IsAttacking}");
            
            View.gameObject.SetActive(true);
            AnimatorEvents.Attacking += HandleAttacking;
            AttackAnimator.SetTrigger(AttackTrigger);
            
            yield return new WaitUntil(() => IsAttacking);
            Debug.Log($"{gameObject.name} IsAttacking = {IsAttacking}");
            
            for (int i = 0; i < AttackStats.AttackCount; i++)
            {
                yield return Attack();
            }

            Debug.Log($"{gameObject.name} recovery");
            yield return waitRecovery;
            
            Debug.Log($"{gameObject.name} done");
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