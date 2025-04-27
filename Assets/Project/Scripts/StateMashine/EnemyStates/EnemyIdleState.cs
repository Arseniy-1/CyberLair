using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyIdleState : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMover _mover;
        private readonly EnemyTargetProvider _enemyTargetProvider;
        private IStateSwitcher _stateSwitcher;
        
        private readonly Animator _animator;
        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");

        public EnemyIdleState(Enemy enemy, EnemyMover mover, EnemyTargetProvider enemyTargetProvider, Animator animator)
        {
            _enemy = enemy;
            _mover = mover;
            _enemyTargetProvider = enemyTargetProvider;
            _animator = animator;
        }
        
        public void Enter()
        {
            _mover.enabled = false;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
        }

        public void Update()
        {
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