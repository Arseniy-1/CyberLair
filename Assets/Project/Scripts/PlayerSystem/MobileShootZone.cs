using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileShootZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnShootButtonPressed;
    
    private bool _isButtonPressed;

    private void OnDisable()
    {
        _isButtonPressed = false;
    }
    
    private void Update()
    {
        if (_isButtonPressed)
        {
            OnShootButtonPressed?.Invoke();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonPressed = false;
    }
}