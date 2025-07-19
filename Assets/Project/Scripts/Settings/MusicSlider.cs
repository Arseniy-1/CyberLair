using YG;

namespace Project.Scripts.Settings
{
    public class MusicSlider : SettingSlider
    {
        public override void Initialize()
        {
            base.Initialize();
        
            Slider.value = YG2.saves.MusicVolume;
        }
    
        protected override void HandleSliderValueChanged(float amount)
        {
            YG2.saves.MusicVolume = amount;
            
            YG2.SaveProgress();
        }
    }
}