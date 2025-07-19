using Project.Scripts.Services.Enum;

namespace Project.Scripts.MessageBroker.CameraMessageBrokers
{
    public struct M_CameraShake
    {
        public M_CameraShake(ShakeID shakeId)
        {
            ShakeID = shakeId;
        }
    
        public ShakeID ShakeID { get; private set; }
    }
}