using YG;

namespace Project.Scripts.Settings
{
    public class SoundToggle : SettingToggle
    {
        public override void Initialize()
        {
            base.Initialize();
        
            Toggle.isOn = YG2.saves.IsSoundsMuted;
        }
    
        protected override void HandleToggle(bool isOn)
        {
            YG2.saves.IsSoundsMuted = isOn;
            
            YG2.SaveProgress();
        }
    }
}