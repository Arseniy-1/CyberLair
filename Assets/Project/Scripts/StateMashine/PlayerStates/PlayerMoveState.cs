using UnityEngine;

namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerMoveState : IState
    {
        private readonly PlayerMover _playerMover;
        private readonly PlayerInputController _playerInputController;
        private readonly WeaponHolder _weaponHolder;
        private readonly TargetScanner _targetScanner;
        private readonly Jumper _jumper;

        private readonly int _walkAnimation = Animator.StringToHash("IsMoving");
        
        private IStateSwitcher _stateSwitcher;
        private Animator _animator;

        public PlayerMoveState(PlayerInputController playerInputController, PlayerMover playerMover,
            WeaponHolder weaponHolder, TargetScanner targetScanner, Jumper jumper)
        {
            _playerMover = playerMover;
            _playerInputController = playerInputController;
            _weaponHolder = weaponHolder;
            _targetScanner = targetScanner;
            _jumper = jumper;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }

        public virtual void Enter()
        {
            _playerMover.enabled = true;
            _playerMover.WalkSound.Play();
            _playerInputController.OnJumpButtonPressed += OnJumpButtonPressed;
            
            _animator.SetBool(_walkAnimation, _playerMover.enabled);
        }

        public virtual void Exit()
        {
            _playerMover.enabled = false;
            _playerMover.WalkSound.Stop();
            _playerInputController.OnJumpButtonPressed -= OnJumpButtonPressed;
            
            _animator.SetBool(_walkAnimation, _playerMover.enabled);
        }

        public virtual void Update()
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