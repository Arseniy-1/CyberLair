using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [RequireComponent(typeof(Jumper))]
    public class EnemyJumpAttacker : EnemyAttacker
    {
        [SerializeField] private EnemyJumpStats _jumpStats;
        
        private Jumper _jumper;
        
        private Vector2 Direction => (EnemyTargetProvider.Player.Position - Position).normalized;

        private void Awake()
        {
            _jumper = GetComponent<Jumper>();
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider, IAttackerStats stats)
        {
            _jumper.Initialize(_jumpStats);
            base.Initialize(enemyTargetProvider, stats);
        }
        
        protected override void Attack()
        {
            _jumper.Jump(Direction);
        }
    }
}