
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        public int BestTime = 0;
        public bool IsCameraShakeEnabled = true;
        public bool IsSoundsMuted = false;
        
        public float MusicVolume = 1.0f;
        public float SoundsVolume = 1.0f;
    }
}
