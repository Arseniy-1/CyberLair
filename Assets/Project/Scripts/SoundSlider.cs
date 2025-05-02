using YG;

public class SoundSlider : SettingSlider
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Slider.value = YandexGame.savesData.SoundsVolume;
    }
    
    protected override void HandleSliderValueChanged(float amount)
    {
        YandexGame.savesData.SoundsVolume = amount;
        YandexGame.SaveProgress();
    }
}