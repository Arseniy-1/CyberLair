using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private EnemyCollisionHandler _laser;
        [SerializeField] private Collider2D _collider;
        
        private bool _isAttacking;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("LaserAttack");
            
            Disable();
            
            _laser.Initialize(Damage);
        }

        public override IEnumerator Performing()
        {
            yield return base.Performing();
            
            Disable();
        }

        protected override IEnumerator Attack()
        {   
            
            yield return new WaitUntil(() => _isAttacking);
            
            _collider.enabled = true;
            View.enabled = true;
            
            AnimatorEvents.Attacking += HandleAttacking;
            _isAttacking = true;
            
            AttackAnimator.SetTrigger(AttackTrigger);
            
            yield return new WaitUntil(() => _isAttacking == false);
        }

        protected override void Disable()
        {
            _collider.enabled = false;
            View.enabled = false;
        }

        private void HandleAttacking()
        {
            AnimatorEvents.Attacking -= HandleAttacking;
            Disable();
            
            _isAttacking = false;
        }

        private void BossStartAttack()
        {
            _isAttacking = true;
        }
    }
}