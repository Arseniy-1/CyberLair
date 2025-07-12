using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private PlayerInputProvider _playerInputProvider;
    private Rigidbody2D _rigidbody2D;
    private IMoverStats _moverStats;

    [field: SerializeField] public AudioID WalkSound { get; private set; } = AudioID.PlayerWalk;
    public bool IsRunning => _playerInputProvider.InputDirection != Vector2.zero;

    private void OnEnable()
    {
        _playerInputProvider.OnMoveButtonPressed += Run;
    }

    private void OnDisable()
    {
        _playerInputProvider.OnMoveButtonPressed -= Run;
    }

    public void Initialize(PlayerInputProvider playerInputProvider, Rigidbody2D rigidbody2D, IMoverStats moverStats)
    {
        _playerInputProvider = playerInputProvider;
        _rigidbody2D = rigidbody2D;
        _moverStats = moverStats;
    }
    
    private void Run()
    {
        _rigidbody2D.velocity = _playerInputProvider.InputDirection.normalized * _moverStats.Speed.CurrentValue;
    }
}
