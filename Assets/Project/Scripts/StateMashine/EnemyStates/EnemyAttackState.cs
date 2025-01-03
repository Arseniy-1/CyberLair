using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyAttackState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private Enemy _enemy;
        private EnemyMover _mover;
        private EnemyAttacker _attacker;

        public EnemyAttackState(Enemy enemy, EnemyMover mover, EnemyAttacker attacker)
        {
            _enemy = enemy;
            _mover = mover;
            _attacker = attacker;
        }

        public void Enter()
        {
            _mover.enabled = false;
            _attacker.AttackPerformed += OnAttackPerformed;
            _attacker.Attack();
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
            _stateSwitcher.SwitchState<EnemyIdleState>();
        }
    }
}