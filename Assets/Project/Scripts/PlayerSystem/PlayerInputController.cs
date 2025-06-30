using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private DeviceControlls _deviceControlls;
    [SerializeField] private DesktopControlls _desktopControlls;
    [SerializeField] private MobileShootZone _shootZone;

    private readonly CompositeDisposable _disposable = new();

    private PlayerInput _playerInput;
    
    public Vector2 InputDirection => _playerInput.Land.Move.ReadValue<Vector2>();
    public event Action OnJumpButtonPressed;
    public event Action OnMoveButtonPressed;
    public event Action OnShootButtonPressed;

    private bool _isDevice;

    private void Awake()
    {
        _isDevice = YandexGame.EnvironmentData.isMobile;
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        SelectControlScheme();
    }

    private void OnEnable()
    {
        _shootZone.OnShootButtonPressed += Shoot;
        _playerInput.Land.Jump.performed += OnJumpPerformed;
        
        MessageBrokerHolder.Game
            .Receive<M_GamePaused>()
            .Subscribe(_ => DisableControlScheme())
            .AddTo(_disposable);
        
        MessageBrokerHolder.Game
            .Receive<M_GameUnpaused>()
            .Subscribe(_ => EnableControlScheme())
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _shootZone.OnShootButtonPressed -= Shoot;
        _playerInput.Land.Jump.performed -= OnJumpPerformed;
        
        _disposable?.Clear();
    }

    private void EnableControlScheme()
    {
        _playerInput.Enable();
    }

    private void DisableControlScheme()
    {
        _playerInput.Disable();
    }
    
    private void Update()
    {
        ReadMovementInput();

        if (_isDevice == false && _playerInput.Land.Shoot.IsPressed())
            Shoot();
    }

    private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
    {
        OnJumpButtonPressed?.Invoke();
    }

    private void ReadMovementInput()
    {
        if (InputDirection != Vector2.zero)
            OnMoveButtonPressed?.Invoke();
    }

    private void SelectControlScheme()
    {
        if (_isDevice)
            _deviceControlls.gameObject.SetActive(true);
        else
            _desktopControlls.gameObject.SetActive(true);
    }

    private void Shoot()
    {
        OnShootButtonPressed?.Invoke();
    }
}