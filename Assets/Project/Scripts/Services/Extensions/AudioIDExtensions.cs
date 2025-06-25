using Project.Scripts.MessageBroker.SoundMessageBrokers;

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