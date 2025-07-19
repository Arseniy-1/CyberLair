using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Project.Scripts.Settings
{
    public class LanguageSelector : MonoBehaviour
    {
        [SerializeField] private List<LanguageSelectorButton> _languageSelectorButtons;

        private void OnDestroy()
        {
            foreach (var languageSelectorButton in _languageSelectorButtons)
                languageSelectorButton.OnLanguageChanged -= HandleButtonClick;   
        }
    
        public void Initialize()
        {
            foreach (var languageSelectorButton in _languageSelectorButtons)
                languageSelectorButton.OnLanguageChanged += HandleButtonClick;
        }

        private void HandleButtonClick(string language)
        {
            YG2.SwitchLanguage(language);
        }
    }
}