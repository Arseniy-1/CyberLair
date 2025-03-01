using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private Laser _laser;
        
        private bool _isAttacking;
        
        public override IEnumerator Attack()
        {
            _laser.gameObject.SetActive(true);
            AnimationEvents.Attacking += HandleAttacking;
            _isAttacking = true;

            while (_isAttacking)
            {
                yield return null;
            }
            
            EndAttack();
        }

        public override void Disable()
        {
            _laser.gameObject.SetActive(false);
        }

        private void HandleAttacking()
        {
            AnimationEvents.Attacking -= HandleAttacking;
            Disable();
            
            _isAttacking = false;
        }
    }
}