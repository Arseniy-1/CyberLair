using Project.Scripts.EnemySystem;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyMoveState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private readonly EnemyMover _mover;
        private readonly Enemy _enemy;

        public EnemyMoveState(Enemy enemy, EnemyMover mover)
        {
            _enemy = enemy;
            _mover = mover;
        }
        
        public void Enter()
        {
            _mover.enabled = true;
        }

        public void Update()
        {
            if (_enemy.IsStunned)
                _stateSwitcher.SwitchState<EnemyStunnedState>();
            
            if(_enemy.HasPlayer == false)
                _stateSwitcher.SwitchState<EnemyIdleState>();
            
            if(_enemy.IsPlayerInRange)
                _stateSwitcher.SwitchState<EnemyAttackState>();
        }

        public void Exit()
        {
            _mover.enabled = false;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
    }
}