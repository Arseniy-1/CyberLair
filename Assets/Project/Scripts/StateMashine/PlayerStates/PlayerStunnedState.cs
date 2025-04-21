namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerStunnedState : IState
    {
        private Player _player;
        private PlayerMover _playerMover;
        private Jumper _playerJumper;

        private IStateSwitcher _stateSwitcher;
        
        public PlayerStunnedState(Player player, PlayerMover playerMover, Jumper playerJumper)
        {
            _player = player;
            _playerMover = playerMover;
            _playerJumper = playerJumper;
        }

        public void Enter()
        {
            _playerMover.enabled = false;
            _playerJumper.enabled = false;
        }

        public void Exit()
        {
            _playerMover.enabled = true;
            _playerJumper.enabled = true;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Update()
        {
        }
    }
}
