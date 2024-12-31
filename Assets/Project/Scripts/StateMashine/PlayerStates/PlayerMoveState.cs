namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerMoveState : IState
    {
        private Player _player;
        private PlayerMover _playerMover;
        private PlayerInputController _playerInputController;

        private IStateSwitcher _stateSwitcher;

        public PlayerMoveState(Player player, PlayerInputController playerInputController, PlayerMover playerMover)
        {
            _player = player;
            _playerMover = playerMover;
            _playerInputController = playerInputController;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            _playerMover.enabled = true;
            _playerInputController.OnJumpButtonPressed += OnJumpButtonPressed;
        }

        public virtual void Exit()
        {
            _playerMover.enabled = false;
            _playerInputController.OnJumpButtonPressed -= OnJumpButtonPressed;
        }

        public virtual void Update()
        {
            if (_player.IsStunned)
                _stateSwitcher.SwitchState<PlayerStunnedState>();

            if (_playerMover.IsRunning == false)
                _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        public void OnJumpButtonPressed()
        {
            _stateSwitcher.SwitchState<PlayerJumpState>();
        }
    }
}
