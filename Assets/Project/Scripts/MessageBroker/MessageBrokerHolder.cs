using UniRx;

public static class MessageBrokerHolder
{
    public static IMessageBroker Enemy { get; private set; } = new MessageBroker();
}