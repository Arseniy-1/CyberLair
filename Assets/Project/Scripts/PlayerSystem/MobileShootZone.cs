using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.PlayerSystem
{
    public class MobileShootZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isButtonPressed;
    
        public event Action OnShootButtonPressed;
    
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
}