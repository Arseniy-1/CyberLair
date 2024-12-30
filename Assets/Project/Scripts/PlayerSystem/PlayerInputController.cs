using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput _payerInput;

    public Vector2 InputDirection => _payerInput.Player.Move.ReadValue<Vector2>();

    public event Action OnJumpButtonPressed;
    public event Action OnMoveButtonPressed;
                        
    public event Action OnShootButtonPressed;
    public event Action OnSwitchButtonPressed;

    private void Awake()
    {
        _payerInput = new PlayerInput();
        _payerInput.Enable();
    }

    private void OnEnable()
    {
        _payerInput.Player.Shoot.performed += OnShootPreformed;
    }

    private void OnDisable()
    {
        _payerInput.Player.Shoot.performed -= OnShootPreformed;
    }

    private void Update()
    {
        ReadIMovemetInput();

        if (_payerInput.Player.Shoot.IsPressed())
            OnShootButtonPressed?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
    {
        OnJumpButtonPressed?.Invoke();
    }

    private void OnShootPreformed(InputAction.CallbackContext callbackContext)
    {
        OnShootButtonPressed?.Invoke();
    }

    private void OnSwitchWeaponPreformed(InputAction.CallbackContext callbackContext)
    {
        OnSwitchButtonPressed?.Invoke();
    }

    private void ReadIMovemetInput()
    {
        OnMoveButtonPressed?.Invoke();
    }
}
