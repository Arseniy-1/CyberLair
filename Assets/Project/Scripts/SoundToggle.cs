using YG;

public class SoundToggl : SettingToggle
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Toggle.isOn = YandexGame.savesData.IsSoundsMuted;
    }
    
    protected override void HandleToggle(bool isOn)
    {
        YandexGame.savesData.IsSoundsMuted = isOn;
        YandexGame.SaveProgress();
    }
}