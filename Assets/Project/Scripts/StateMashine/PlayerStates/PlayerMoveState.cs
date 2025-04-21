namespace StateMashineSytem.PlayerStateMashine
{
    public class PlayerMoveState : IState
    {
        private Player _player;
        private PlayerMover _playerMover;
        private PlayerInputController _playerInputController;
        private WeaponHolder _weaponHolder;
        private TargetScanner _targetScanner;
        private Jumper _jumper;

        private IStateSwitcher _stateSwitcher;

        public PlayerMoveState(Player player, PlayerInputController playerInputController, PlayerMover playerMover,
            WeaponHolder weaponHolder, TargetScanner targetScanner, Jumper jumper)
        {
            _player = player;
            _playerMover = playerMover;
            _playerInputController = playerInputController;
            _weaponHolder = weaponHolder;
            _targetScanner = targetScanner;
            _jumper = jumper;
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
            if (_targetScanner.HasTarget)
                _weaponHolder.SpotTarget(_targetScanner.ClosestTarget);

            if (_playerMover.IsRunning == false)
                _stateSwitcher.SwitchState<PlayerIdleState>();
        }

        public void OnJumpButtonPressed()
        {
            if (_jumper.CanJump)
                _stateSwitcher.SwitchState<PlayerJumpState>();
        }
    }
}