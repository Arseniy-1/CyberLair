using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private EnemyCollisionHandler _laser;
        
        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        private bool _isAttacking;

        public void OnEnable()
        {
            View.gameObject.SetActive(false);
            _laser.Initialize(Damage);
        }

        protected override IEnumerator Attack()
        {
            View.gameObject.SetActive(true);
            View.Attacking += HandleAttacking;
            _isAttacking = true;
            Animator.SetTrigger(_attackTrigger);

            while (_isAttacking)
            {
                yield return null;
            }
        }

        protected override void Disable()
        {
            View.gameObject.SetActive(false);
        }

        private void HandleAttacking()
        {
            View.Attacking -= HandleAttacking;
            Disable();
            
            _isAttacking = false;
        }
    }
}