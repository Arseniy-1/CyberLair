using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileShootZone : MonoBehaviour, IPointerDownHandler
{
    public event Action OnShootButtonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log("OnPointerDown");
        OnShootButtonPressed?.Invoke();
    }
}