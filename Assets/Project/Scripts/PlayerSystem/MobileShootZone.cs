using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileShootZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnShootButtonPressed;
    
    private bool _isButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonPressed = false;
    }

    private void FixedUpdate()
    {
        if (_isButtonPressed)
        {
            OnShootButtonPressed?.Invoke();
        }
    }
}