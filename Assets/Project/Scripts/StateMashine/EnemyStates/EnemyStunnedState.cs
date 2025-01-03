using Project.Scripts.EnemySystem;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyStunnedState : IState
    {
        private Enemy _enemy;
        private EnemyMover _mover;

        private IStateSwitcher _stateSwitcher;
        
        public EnemyStunnedState(Enemy enemy, EnemyMover mover)
        {
            _enemy = enemy;
            _mover = mover;
        }
        
        public void Enter()
        {
            _mover.enabled = false;
        }

        public void Update()
        {
            if (_enemy.IsStunned == false)
                _stateSwitcher.SwitchState<EnemyIdleState>();
        }

        public void Exit()
        {
            _mover.enabled = true;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
    }
}