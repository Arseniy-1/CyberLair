using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private EnemyCollisionHandler _laser;
        [SerializeField] private Collider2D _collider;
        
        private bool _isAttacking;

        public void OnEnable()
        {
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
            _collider.enabled = true;
            View.enabled = true;
            
            AnimatorEvents.Attacking += HandleAttacking;
            _isAttacking = true;
            
            Animator.SetTrigger(AttackTrigger);

            while (_isAttacking)
            {
                yield return null;
            }
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
    }
}