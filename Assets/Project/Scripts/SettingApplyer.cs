using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingApplyer : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Toggle _cameraShakeToggle;
    [SerializeField] private Toggle _muteToggle;
    
    private void Start()
    {
        _musicSlider.value = YandexGame.savesData.MusicVolume;
        _soundSlider.value = YandexGame.savesData.SoundsVolume;
        _cameraShakeToggle.isOn = YandexGame.savesData.IsCameraShakeEnabled;
        _muteToggle.isOn = YandexGame.savesData.IsSoundsMuted;
    }

}