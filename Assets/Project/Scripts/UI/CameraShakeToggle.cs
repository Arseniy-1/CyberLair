using YG;

public class CameraShakeToggle : SettingToggle
{
    public override void Initialize()
    {
        base.Initialize();
        Toggle.isOn = YandexGame.savesData.IsCameraShakeEnabled;
    }
    
    protected override void HandleToggle(bool isOn)
    {
        YandexGame.savesData.IsCameraShakeEnabled = isOn;
        
        YandexGame.SaveProgress();
    }
}