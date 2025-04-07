using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyAttackState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private readonly EnemyMover _mover;
        private readonly EnemyAttacker _attacker;
        private readonly EnemyAttackCooldown _cooldown;

        public EnemyAttackState(EnemyMover mover, EnemyAttacker attacker, EnemyAttackCooldown cooldown)
        {
            _mover = mover;
            _attacker = attacker;
            _cooldown = cooldown;
        }

        public void Enter()
        {
            _mover.enabled = false;
            _attacker.AttackPerformed += OnAttackPerformed;
            _attacker.PerformAttack();
        }

        public void Update() { }

        public void Exit()
        {
            _attacker.AttackPerformed -= OnAttackPerformed;
            _mover.enabled = true;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        private void OnAttackPerformed()
        {
            _cooldown.StartCooldown(_attacker.Stats.Cooldown);
            _stateSwitcher.SwitchState<EnemyIdleState>();
        }
    }
}