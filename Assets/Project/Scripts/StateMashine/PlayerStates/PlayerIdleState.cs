using Project.Scripts.Interfaces;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.StateMashine.PlayerStates
{
    public class PlayerIdleState : IState
    {
        private readonly PlayerMover _playerMover;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly WeaponHolder _weaponHolder;
        private readonly TargetScanner _targetScanner;
        
        private readonly int _walkAnimation = Animator.StringToHash("IsMoving");
        
        private IStateSwitcher _stateSwitcher;
        private Animator _animator;

        public PlayerIdleState(
            PlayerMover playerMover, 
            Rigidbody2D rigidbody2D,
            WeaponHolder weaponHolder, 
            TargetScanner targetScanner)
        {
            _playerMover = playerMover;
            _rigidbody2D = rigidbody2D;
            _weaponHolder = weaponHolder;
            _targetScanner = targetScanner;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }

        public void Enter()
        {
            _rigidbody2D.velocity = Vector3.zero;
            
            _animator.SetBool(_walkAnimation, false);
        }

        public void Exit() { }

        public void Update()
        {
            if (_targetScanner.HasTarget)
                _weaponHolder.SpotTarget(_targetScanner.ClosestTarget);

            if (_playerMover.IsRunning)
                _stateSwitcher.SwitchState<PlayerMoveState>();
        }
    }
}