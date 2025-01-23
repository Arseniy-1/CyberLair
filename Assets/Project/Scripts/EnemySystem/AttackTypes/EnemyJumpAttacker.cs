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

        public override void Initialize(EnemyTargetProvider enemyTargetProvider, EnemyStats stats)
        {
            _jumper.Initialize(stats);
            base.Initialize(enemyTargetProvider, stats);
        }
        
        protected override void Attack()
        {
            Debug.Log($"{transform.name} is attacking");
            _jumper.Jump(Direction);
        }
    }
}