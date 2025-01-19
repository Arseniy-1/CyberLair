using UnityEngine;

namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerJumpState : IState
    {
        private Jumper _jumper;
        private PlayerInputController _playerInputController;
        private Collider2D _collider2D;

        private IStateSwitcher _stateSwitcher;

        public PlayerJumpState(PlayerInputController playerInputController, Collider2D collider2D, Jumper jumper)
        {
            _playerInputController = playerInputController;
            _collider2D = collider2D;
            _jumper = jumper;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            _playerInputController.enabled = false;
            _collider2D.enabled = false;
            _jumper.Jump(_playerInputController.InputDirection);
            _jumper.JumpPerformed += OnJumpPerformed;
        }

        public virtual void Exit()
        {
            _playerInputController.enabled = true;
            _collider2D.enabled = true;
            _jumper.JumpPerformed -= OnJumpPerformed;
        }

        public virtual void Update()
        {

        }

        public void OnJumpPerformed()
        {
            _stateSwitcher.SwitchState<PlayerIdleState>();
        }
    }
}
