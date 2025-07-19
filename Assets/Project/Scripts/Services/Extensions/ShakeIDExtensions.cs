using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Project.Scripts.Services.Enum;

namespace Project.Scripts.Services.Extensions
{
    public static class ShakeIDExtensions
    {
        public static void Shake(this ShakeID shakeID)
        {
            MessageBrokerHolder.Camera
                .Publish(new M_CameraShake(shakeID));
        }
    }
}