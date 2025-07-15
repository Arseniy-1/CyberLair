using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Settings
{
    public class LanguageSelectorButton : MonoBehaviour
    {
        [SerializeField] private Languages _language;
   
        private Button _button;
    
        public event Action<string> OnLanguageChanged;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleLanguageChanged);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleLanguageChanged);
        }

        private void HandleLanguageChanged()
        {
            OnLanguageChanged?.Invoke(_language.ToString());
        }
    }
}