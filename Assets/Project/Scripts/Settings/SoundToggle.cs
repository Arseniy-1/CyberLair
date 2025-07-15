using YG;

namespace Project.Scripts.Settings
{
    public class SoundToggle : SettingToggle
    {
        public override void Initialize()
        {
            base.Initialize();
        
            Toggle.isOn = YandexGame.savesData.IsSoundsMuted;
        }
    
        protected override void HandleToggle(bool isOn)
        {
            YandexGame.savesData.IsSoundsMuted = isOn;
            YandexGame.SaveProgress();
        }
    }
}