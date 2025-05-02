using YG;

public class CameraShakeToggle : SettingToggle
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Toggle.isOn = YandexGame.savesData.IsCameraShakeEnabled;
    }
    
    protected override void HandleToggle(bool isOn)
    {
        YandexGame.savesData.IsCameraShakeEnabled = isOn;
        YandexGame.SaveProgress();
    }
}