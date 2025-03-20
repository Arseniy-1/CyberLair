using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private EnemyCollisionHandler _laser;
        [SerializeField] private Collider2D _collider;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("LaserAttack");
            
            Disable();
            
            _laser.Initialize(Damage);
        }

        protected override IEnumerator Attack()
        {   
            _collider.enabled = true;
            
            yield return new WaitUntil(() => IsAttacking == false);
            
            Disable();
        }

        protected override void Disable()
        {
            _collider.enabled = false;
            View.enabled = false;
        }
    }
}