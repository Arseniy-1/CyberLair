using YG;

namespace Project.Scripts.Settings
{
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
}