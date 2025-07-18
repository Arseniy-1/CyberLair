using YG;

namespace Project.Scripts.Settings
{
    public class SoundSlider : SettingSlider
    {
        public override void Initialize()
        {
            base.Initialize();
        
            Slider.value = YG2.saves.SoundsVolume;
        }

        protected override void HandleSliderValueChanged(float amount)
        {
            YG2.saves.SoundsVolume = amount;
            
            YG2.SaveProgress();
        }
    }
}