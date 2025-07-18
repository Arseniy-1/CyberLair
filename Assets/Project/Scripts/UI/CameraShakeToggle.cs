using Project.Scripts.Settings;
using YG;

namespace Project.Scripts.UI
{
    public class CameraShakeToggle : SettingToggle
    {
        public override void Initialize()
        {
            base.Initialize();
        
            Toggle.isOn = YG2.saves.IsCameraShakeEnabled;
        }
    
        protected override void HandleToggle(bool isOn)
        {
            YG2.saves.IsCameraShakeEnabled = isOn;
            
            YG2.SaveProgress();
        }
    }
}