using Project.Scripts.Interfaces;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.StateMashine.PlayerStates
{
    public class PlayerMoveState : IState
    {
        private readonly PlayerMover _playerMover;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly WeaponHolder _weaponHolder;
        private readonly TargetScanner _targetScanner;
        private readonly Jumper _jumper;

        private readonly int _walkAnimation = Animator.StringToHash("IsMoving");
        
        private IStateSwitcher _stateSwitcher;
        private Animator _animator;

        public PlayerMoveState(
            PlayerInputProvider playerInputProvider, 
            PlayerMover playerMover,
            WeaponHolder weaponHolder, 
            TargetScanner targetScanner, 
            Jumper jumper)
        {
            _playerMover = playerMover;
            _playerInputProvider = playerInputProvider;
            _weaponHolder = weaponHolder;
            _targetScanner = targetScanner;
            _jumper = jumper;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }

        public void Enter()
        {
            _playerMover.enabled = true;
            _playerMover.WalkSound.Play();
            _playerInputProvider.OnJumpButtonPressed += OnJumpButtonPressed;
            
            _animator.SetBool(_walkAnimation, _playerMover.enabled);
        }

        public void Exit()
        {
            _playerMover.enabled = false;
            _playerMover.WalkSound.Stop();
            _playerInputProvider.OnJumpButtonPressed -= OnJumpButtonPressed;
            
            _animator.SetBool(_walkAnimation, _playerMover.enabled);
        }

        public void Update()
        {
            if (_targetScanner.HasTarget)
                _weaponHolder.SpotTarget(_targetScanner.ClosestTarget);

            if (_playerMover.IsRunning == false)
                _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        private void OnJumpButtonPressed()
        {
            if (_jumper.CanJump)
                _stateSwitcher.SwitchState<PlayerJumpState>();
        }
    }
}