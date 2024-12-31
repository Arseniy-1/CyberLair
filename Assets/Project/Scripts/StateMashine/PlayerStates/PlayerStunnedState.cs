namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerStunnedState : IState
    {
        private Player _player;
        private PlayerMover _playerMover;

        private IStateSwitcher _stateSwitcher;
        
        public PlayerStunnedState(Player player, PlayerMover playerMover)
        {
            _player = player;
            _playerMover = playerMover;
        }

        public void Enter()
        {
            _playerMover.enabled = false;
        }

        public void Exit()
        {
            _playerMover.enabled = true;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Update()
        {
            if (_player.IsStunned == false)
                _stateSwitcher.SwitchState<PlayerIdleState>();
        }
    }
}
