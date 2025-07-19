using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.SoundMessageBrokers;
using Project.Scripts.Services.Enum;

namespace Project.Scripts.Services.Extensions
{
    public static class AudioIDExtensions
    {
        public static void Play(this AudioID audioID)
        {
            MessageBrokerHolder.Audio
                .Publish(new M_PlayAudio(audioID));
        }
    
        public static void Stop(this AudioID audioID)
        {
            MessageBrokerHolder.Audio
                .Publish(new M_StopAudio(audioID));
        }
    }
}