using System;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private DeviceControlls _deviceControlls;
    [SerializeField] private DesktopControlls _desktopControlls;
    [SerializeField] private MobileShootZone _shootZone;

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
        _playerInput.Land.Shoot.performed += OnShootPerformed;
        _playerInput.Land.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _shootZone.OnShootButtonPressed -= Shoot;
        _playerInput.Land.Shoot.performed -= OnShootPerformed;
        _playerInput.Land.Jump.performed -= OnJumpPerformed;
    }

    private void Update()
    {
        ReadMovementInput();

        if (!_isDevice && _playerInput.Land.Shoot.IsPressed())
        {
            OnShootButtonPressed?.Invoke();
        }

        // if (_isDevice)
        // {
        //     HandleMobileShooting();
        // }
    }

    private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
    {
        OnJumpButtonPressed?.Invoke();
    }

    private void OnShootPerformed(InputAction.CallbackContext callbackContext)
    {
        if (!_isDevice)
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
        if (_isDevice)
            _deviceControlls.gameObject.SetActive(true);
        else
            _desktopControlls.gameObject.SetActive(true);
    }

    private void HandleMobileShooting()
    {
        _shootZone.OnShootButtonPressed += Shoot;
    }

    private void Shoot()
    {
        OnShootButtonPressed?.Invoke();
    }
}