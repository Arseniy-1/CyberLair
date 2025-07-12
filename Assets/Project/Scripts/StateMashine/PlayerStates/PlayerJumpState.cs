using UnityEngine;

namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerJumpState : IState
    {
        private readonly Jumper _jumper;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly Collider2D _collider2D;

        private readonly int _jumpTrigger = Animator.StringToHash("Jump");
        
        private IStateSwitcher _stateSwitcher;
        private Animator _animator;

        public PlayerJumpState(PlayerInputProvider playerInputProvider, Collider2D collider2D, Jumper jumper)
        {
            _playerInputProvider = playerInputProvider;
            _collider2D = collider2D;
            _jumper = jumper;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }

        public void Enter()
        {
            _playerInputProvider.enabled = false;
            _collider2D.enabled = false;
            _jumper.Jump(_playerInputProvider.InputDirection);
            _jumper.JumpPerformed += OnJumpPerformed;
            
            _animator.SetTrigger(_jumpTrigger);
        }

        public void Exit()
        {
            _playerInputProvider.enabled = true;
            _collider2D.enabled = true;
            _jumper.JumpPerformed -= OnJumpPerformed;
            
            _animator.ResetTrigger(_jumpTrigger);
        }

        public void Update() {}

        private void OnJumpPerformed()
        {
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }
    }
}
