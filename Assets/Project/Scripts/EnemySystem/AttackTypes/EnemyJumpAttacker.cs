using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [RequireComponent(typeof(Jumper))]
    public class EnemyJumpAttacker : EnemyAttacker
    {
        private Jumper _jumper;
        
        private Vector2 Direction => (EnemyTargetProvider.Player.Position - Position).normalized;

        private void Awake()
        {
            _jumper = GetComponent<Jumper>();
        }
        
        protected override void Attack()
        {
            _jumper.Jump(Direction);
        }
    }
}