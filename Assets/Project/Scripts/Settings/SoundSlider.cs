using UnityEngine;
using YG;

public class SoundSlider : SettingSlider
{
    public override void Initialize()
    {
        base.Initialize();
        Slider.value = YandexGame.savesData.SoundsVolume;
    }

    protected override void HandleSliderValueChanged(float amount)
    {
        YandexGame.savesData.SoundsVolume = amount;
        YandexGame.SaveProgress();
    }
}