using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class UnpauseButton : MonoBehaviour
    {
        private Button _button;

        public event Action OnUnpause;
    
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Unpause);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Unpause);
        }

        private void Unpause()
        {
            OnUnpause?.Invoke();
        }
    }
}