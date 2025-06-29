using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    [RequireComponent(typeof(Jumper))]
    public class EnemyJumpAttacker : EnemyAttacker
    {
        [SerializeField] private EnemyJumpStats _jumpStats;
        
        private Jumper _jumper;
        private WaitForSeconds _waitForJump;
        
        private Vector2 Direction => (EnemyTargetProvider.Player.Position - Position).normalized;

        private void Awake()
        {
            _jumper = GetComponent<Jumper>();
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            _jumpStats.JumpTime.CalculateCurrentValue();
            _jumpStats.JumpSpeed.CalculateCurrentValue();
            _jumpStats.JumpReloadTime.CalculateCurrentValue();
            
            _jumper.Initialize(_jumpStats);
            _waitForJump = new WaitForSeconds(_jumpStats.JumpTime.CurrentValue);
            base.Initialize(enemyTargetProvider);
        }
        
        protected override IEnumerator Attack()
        {
            _jumper.Jump(Direction);
            yield return _waitForJump;
        }
    }
}