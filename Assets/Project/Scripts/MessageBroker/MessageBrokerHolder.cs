using UniRx;

namespace Project.Scripts.MessageBroker
{
    public static class MessageBrokerHolder
    {
        public static IMessageBroker Enemy { get; private set; } = new UniRx.MessageBroker();
        public static IMessageBroker Camera { get; private set; } = new UniRx.MessageBroker();
        public static IMessageBroker Chest { get; private set; } = new UniRx.MessageBroker();
        public static IMessageBroker Game { get; private set; } = new UniRx.MessageBroker();
        public static IMessageBroker Audio { get; private set; } = new UniRx.MessageBroker();
    }
}