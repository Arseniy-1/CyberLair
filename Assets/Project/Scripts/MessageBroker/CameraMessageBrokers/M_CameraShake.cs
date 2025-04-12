namespace Project.Scripts.MessageBroker.CameraMessageBrokers
{
    public struct M_CameraShake
    {
        public M_CameraShake(CameraShakeSettings shakeSettings)
        {
            ShakeSettings = shakeSettings;
        }
    
        public CameraShakeSettings ShakeSettings { get; private set; }
    }
}