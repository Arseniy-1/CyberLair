using System;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private DeviceControlls _deviceControlls;
    [SerializeField] private DesktopControlls _desktopControlls;

    private PlayerInput _playerInput;

    public Vector2 InputDirection => _playerInput.Land.Move.ReadValue<Vector2>();

    public event Action OnJumpButtonPressed;
    public event Action OnMoveButtonPressed;
    public event Action OnShootButtonPressed;
    public event Action OnSwitchButtonPressed;

    public bool _isMobile;

    private void Awake()
    {
        _isMobile = YandexGame.EnvironmentData.isMobile;
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        SelectControlScheme();
    }

    private void OnEnable()
    {
        _playerInput.Land.Shoot.performed += OnShootPerformed;
        _playerInput.Land.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Land.Shoot.performed -= OnShootPerformed;
        _playerInput.Land.Jump.performed -= OnJumpPerformed;
    }

    private void Update()
    {
        ReadMovementInput();

        if (!_isMobile && _playerInput.Land.Shoot.IsPressed()) // ПК и любое нажатие вызывает атаку
        {
            OnShootButtonPressed?.Invoke();
        }

        if (_isMobile)
        {
            HandleMobileShooting();
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
    {
        OnJumpButtonPressed?.Invoke();
    }

    private void OnShootPerformed(InputAction.CallbackContext callbackContext)
    {
        if (!_isMobile)
        {
            OnShootButtonPressed?.Invoke();
        }
    }

    private void ReadMovementInput()
    {
        if (InputDirection != Vector2.zero)
        {
            OnMoveButtonPressed?.Invoke();
        }
    }

    private void SelectControlScheme()
    {
        if (_isMobile)
            _deviceControlls.gameObject.SetActive(true);
        else
            _desktopControlls.gameObject.SetActive(true);
    }

    private void HandleMobileShooting()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        if (touchPosition.x > Screen.width / 2)
        {
            OnShootButtonPressed?.Invoke();
        }
    }
}
