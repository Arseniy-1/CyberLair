using System.Collections.Generic;
using Project.Scripts.Settings;
using UnityEngine;
using YG;

public class SettingsInitializer : MonoBehaviour
{
    [SerializeField] private ExtendedVolumeChanger _extendedVolumeChanger;
    [SerializeField] private VolumeChanger _volumeChanger;
    [SerializeField] private LanguageSelector _languageSelector;
    [SerializeField] private List<SettingSlider> _sliders;
    [SerializeField] private List<SettingToggle> _settingToggles;

    private void Start()
    {
        YandexGame.LoadProgress();
        YandexGame.SwitchLanguage(YandexGame.lang);
        
        _extendedVolumeChanger.Initialize();
        _volumeChanger.Initialize();

        foreach (var slider in _sliders)
            slider.Initialize();

        foreach (var toggle in _settingToggles)
            toggle.Initialize();
        
        _languageSelector.Initialize();
    }
}