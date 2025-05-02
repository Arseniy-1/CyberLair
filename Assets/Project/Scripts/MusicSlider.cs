using UnityEngine.InputSystem.Interactions;
using YG;

public class MusicSlider : SettingSlider
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Slider.value = YandexGame.savesData.MusicVolume;
    }
    
    protected override void HandleSliderValueChanged(float amount)
    {
        YandexGame.savesData.MusicVolume = amount;
        YandexGame.SaveProgress();
    }
}