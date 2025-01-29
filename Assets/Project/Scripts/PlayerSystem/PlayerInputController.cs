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
    public event Action OnAttackPerformed;

    private Rect attackArea;

    private void Awake()
    {
        _payerInput = new PlayerInput();
        _payerInput.Enable();

        attackArea = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height);
    }

    private void OnEnable()
    {
        _payerInput.Player.Shoot.performed += OnShootPreformed;
        _payerInput.Player.Jump.performed += OnJumpPerformed;

        // _payerInput.Player.Touchscreen.primaryTouch.performed += OnTouchPerformed;
    }

    private void OnDisable()
    {
        _payerInput.Player.Shoot.performed -= OnShootPreformed;
        _payerInput.Player.Jump.performed -= OnJumpPerformed;

        // _payerInput.Player.Touchscreen.primaryTouch.performed -= OnTouchPerformed;
    }

    private void Update()
    {
        ReadMovementInput();

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

    private void OnTouchPerformed(InputAction.CallbackContext callbackContext)
    {
        Vector2 touchPosition = callbackContext.ReadValue<Vector2>();

        if (touchPosition.x < Screen.width / 2)
        {
            float moveX = (touchPosition.x / (Screen.width / 2)) * 2 - 1;
            Vector2 moveDirection = new Vector2(moveX, 0);

            OnMoveButtonPressed?.Invoke();
        }
    }

    private void ReadMovementInput()
    {
        if (InputDirection != Vector2.zero)
        {
            OnMoveButtonPressed?.Invoke();
        }
    }
}
