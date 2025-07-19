using Project.Scripts.Services.Enum;

namespace Project.Scripts.MessageBroker.SoundMessageBrokers
{
    public struct M_StopAudio
    {
        public M_StopAudio(AudioID audioID)
        {
            AudioID = audioID;
        }
        
        public AudioID AudioID { get; }
    }
}