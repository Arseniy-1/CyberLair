using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMover : MonoBehaviour
{
    [field: SerializeField] public SoundPlayer WalkSoundPlayer { get; private set; }
    
    private PlayerInputController _playerInputController;
    private Rigidbody2D _rigidbody2D;
    private IMoverStats _moverStats;
    public bool IsRunning => _playerInputController.InputDirection != Vector2.zero;

    private void OnEnable()
    {
        _playerInputController.OnMoveButtonPressed += Run;
    }

    private void OnDisable()
    {
        _playerInputController.OnMoveButtonPressed -= Run;
    }

    public void Initialize(PlayerInputController playerInputController, Rigidbody2D rigidbody2D, IMoverStats moverStats)
    {
        _playerInputController = playerInputController;
        _rigidbody2D = rigidbody2D;
        _moverStats = moverStats;
    }
    
    private void Run()
    {
        _rigidbody2D.velocity = _playerInputController.InputDirection.normalized * _moverStats.Speed.CurrentValue;
    }
}
