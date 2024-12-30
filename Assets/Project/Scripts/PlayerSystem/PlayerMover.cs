using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private PlayerInputController _playerInputController;
    private Rigidbody2D _rigidbody2D;

    public bool IsRunning => _playerInputController.InputDirection != Vector2.zero;

    private void OnEnable()
    {
        _playerInputController.OnMoveButtonPressed += Run;
    }

    private void OnDisable()
    {
        _playerInputController.OnMoveButtonPressed -= Run;
    }

    private void Update()
    {
        Run();
    }

    public void Initialize(PlayerInputController playerInputController, Rigidbody2D rigidbody2D)
    {
        _playerInputController = playerInputController;
        _rigidbody2D = rigidbody2D;
    }

    public void Run()
    {
        _rigidbody2D.velocity = _playerInputController.InputDirection.normalized * _speed;
    }
}
