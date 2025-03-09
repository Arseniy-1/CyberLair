using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private EnemyCollisionHandler _laser;
        [SerializeField] private Collider2D _collider;
        
        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        private bool _isAttacking;

        public void OnEnable()
        {
            Disable();
            
            _laser.Initialize(Damage);
        }

        protected override IEnumerator Attack()
        {
            Debug.Log("Laser Attacks");
            
            _collider.enabled = true;
            View.enabled = true;
            
            AnimatorEvents.Attacking += HandleAttacking;
            _isAttacking = true;
            
            Animator.SetTrigger(_attackTrigger);

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