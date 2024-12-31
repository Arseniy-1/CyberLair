using UnityEngine;

namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerIdleState : IState
    {
        private Player _player;
        private PlayerMover _playerMover;
        private Rigidbody2D _rigidbody2D;

        private IStateSwitcher _stateSwitcher;

        public PlayerIdleState(Player player, PlayerMover playerMover, Rigidbody2D rigidbody2D)
        {
            _player = player;
            _playerMover = playerMover;
            _rigidbody2D = rigidbody2D;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            _rigidbody2D.velocity = Vector3.zero;
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            if (_player.IsStunned)
                _stateSwitcher.SwitchState<PlayerStunnedState>();

            if (_playerMover.IsRunning)
                _stateSwitcher.SwitchState<PlayerMoveState>();
        }
    }
}
