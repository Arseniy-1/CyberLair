using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyStunnedState : IState
    {
        private readonly EnemyMover _mover;

        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
        private Animator _animator;
        
        public EnemyStunnedState(EnemyMover mover)
        {
            _mover = mover;
        }
        
        public void Enter()
        {
            _mover.enabled = false;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
        }
        
        public void Exit()
        {
            _mover.enabled = true;
        }
        
        public void Update() { }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _animator = animator;
        }
    }
}