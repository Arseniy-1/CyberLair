using Project.Scripts.Services.Enum;

namespace Project.Scripts.MessageBroker.SoundMessageBrokers
{
    public struct M_PlayAudio
    {
        public M_PlayAudio(AudioID audioID)
        {
            AudioID = audioID;
        }
        
        public AudioID AudioID { get; }
    }
}