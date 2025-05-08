using YG;

public class MusicSlider : SettingSlider
{
    public override void Initialize()
    {
        base.Initialize();
        Slider.value = YandexGame.savesData.MusicVolume;
    }
    
    protected override void HandleSliderValueChanged(float amount)
    {
        YandexGame.savesData.MusicVolume = amount;
        YandexGame.SaveProgress();
    }
}