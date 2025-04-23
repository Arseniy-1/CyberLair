using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyStunnedState : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMover _mover;
        private IStateSwitcher _stateSwitcher;

        private readonly Animator _animator;
        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
        
        public EnemyStunnedState(Enemy enemy, EnemyMover mover, Animator animator)
        {
            _enemy = enemy;
            _mover = mover;
            _animator = animator;
        }
        
        public void Enter()
        {
            _mover.enabled = false;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
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