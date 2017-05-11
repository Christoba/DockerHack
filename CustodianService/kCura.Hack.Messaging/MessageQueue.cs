using NATS.Client;

namespace kCura.Hack.Messaging
{
    public static class MessageQueue
    {
        public const string MessageQueueUrl = "nats://message-queue:4222";

        public static void Publish<TMessage>(TMessage message)
            where TMessage : Message
        {
            using (var connection = CreateConnection())
            {
                var data = MessageHelper.ToData(message);
                connection.Publish(message.Subject, data);
            }
        }

        public static IConnection CreateConnection()
        {
            return new ConnectionFactory().CreateConnection(MessageQueueUrl);
        }
    }
}
