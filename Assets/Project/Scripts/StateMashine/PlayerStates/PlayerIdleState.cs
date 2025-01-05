using TMPro;
using UnityEngine;

namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerIdleState : IState
    {
        private Player _player;
        private PlayerMover _playerMover;
        private Rigidbody2D _rigidbody2D;
        private WeaponHolder _weaponHolder;
        private TargetScanner _targetScanner;

        private IStateSwitcher _stateSwitcher;

        public PlayerIdleState(Player player, PlayerMover playerMover, Rigidbody2D rigidbody2D,
            WeaponHolder weaponHolder, TargetScanner targetScanner)
        {
            _player = player;
            _playerMover = playerMover;
            _rigidbody2D = rigidbody2D;
            _weaponHolder = weaponHolder;
            _targetScanner = targetScanner;
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
            if (_targetScanner.HasTarget)
                _weaponHolder.SpotTarget(_targetScanner.ClosestTarget);

            if (_player.IsStunned)
                _stateSwitcher.SwitchState<PlayerStunnedState>();

            if (_playerMover.IsRunning)
                _stateSwitcher.SwitchState<PlayerMoveState>();
        }
    }
}