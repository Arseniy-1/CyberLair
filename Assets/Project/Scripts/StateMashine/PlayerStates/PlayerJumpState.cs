using UnityEngine;

namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerJumpState : IState
    {
        private readonly Jumper _jumper;
        private readonly PlayerInputController _playerInputController;
        private readonly Collider2D _collider2D;

        private readonly int _jumpTrigger = Animator.StringToHash("Jump");
        
        private IStateSwitcher _stateSwitcher;
        private Animator _animator;

        public PlayerJumpState(PlayerInputController playerInputController, Collider2D collider2D, Jumper jumper)
        {
            _playerInputController = playerInputController;
            _collider2D = collider2D;
            _jumper = jumper;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }

        public virtual void Enter()
        {
            _playerInputController.enabled = false;
            _collider2D.enabled = false;
            _jumper.Jump(_playerInputController.InputDirection);
            _jumper.JumpPerformed += OnJumpPerformed;
            
            _animator.SetTrigger(_jumpTrigger);
        }

        public virtual void Exit()
        {
            _playerInputController.enabled = true;
            _collider2D.enabled = true;
            _jumper.JumpPerformed -= OnJumpPerformed;
            
            _animator.ResetTrigger(_jumpTrigger);
        }

        public virtual void Update() {}

        private void OnJumpPerformed()
        {
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }
    }
}
