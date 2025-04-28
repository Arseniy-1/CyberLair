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
        
        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
        private readonly int _attackAnimation = Animator.StringToHash("IsAttacking");
        private  Animator _animator;

        public EnemyIdleState(Enemy enemy, EnemyMover mover, EnemyTargetProvider enemyTargetProvider)
        {
            _enemy = enemy;
            _mover = mover;
            _enemyTargetProvider = enemyTargetProvider;
        }
        
        public void Enter()
        {
            _mover.enabled = false;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
            _animator.SetBool(_attackAnimation, false);
        }

        public void Update()
        {
            if(_enemyTargetProvider.HasPlayer)
                _stateSwitcher.SwitchState<EnemyMoveState>();
        }

        public void Exit() { }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }
    }
}