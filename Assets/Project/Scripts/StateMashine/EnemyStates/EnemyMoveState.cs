using Project.Scripts.EnemySystem;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyMoveState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private readonly EnemyMover _mover;
        private readonly Enemy _enemy;
        private readonly EnemyTargetProvider _enemyTargetProvider;
        private readonly EnemyAttackCooldown _cooldown;

        public EnemyMoveState(Enemy enemy, EnemyMover mover, EnemyTargetProvider enemyTargetProvider, EnemyAttackCooldown cooldown)
        {
            _enemy = enemy;
            _mover = mover;
            _enemyTargetProvider = enemyTargetProvider;
            _cooldown = cooldown;
        }
        
        public void Enter()
        {
            _mover.enabled = true;
        }

        public void Update()
        {
            if (_enemy.IsStunned)
                _stateSwitcher.SwitchState<EnemyStunnedState>();
            
            if(_enemyTargetProvider.HasPlayer == false)
                _stateSwitcher.SwitchState<EnemyIdleState>();
            
            if(_enemyTargetProvider.IsPlayerInRange && _cooldown.IsOnCooldown == false)
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