using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyIdleState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private Enemy _enemy;
        private Rigidbody2D _rigidbody;
        private EnemyTargetProvider _enemyTargetProvider;

        public EnemyIdleState(Enemy enemy, Rigidbody2D rigidbody, EnemyTargetProvider enemyTargetProvider)
        {
            _enemy = enemy;
            _rigidbody = rigidbody;
            _enemyTargetProvider = enemyTargetProvider;
        }
        
        public void Enter()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public void Update()
        {
            if (_enemy.IsStunned)
                _stateSwitcher.SwitchState<EnemyStunnedState>();
            
            if(_enemyTargetProvider.HasPlayer)
                _stateSwitcher.SwitchState<EnemyMoveState>();
        }

        public void Exit() { }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
    }
}