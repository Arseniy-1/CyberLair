using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [RequireComponent(typeof(Jumper))]
    public class EnemyJumpAttacker : EnemyAttacker
    {
        
        private Jumper _jumper;

        private void Awake()
        {
            _jumper = GetComponent<Jumper>();
        }
        
        public override void Attack()
        {
            _jumper.JumpPerformed += EndAttack;
            _jumper.Jump(Direction);
        }

        protected override void EndAttack()
        {
            _jumper.JumpPerformed -= EndAttack;
            
            base.EndAttack();
        }
    }
}