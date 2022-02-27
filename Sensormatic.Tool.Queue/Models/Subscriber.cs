using RabbitMQ.Client;

namespace Sensormatic.Tool.Queue
{
    public class Subscriber
    {
        public IModel Channel { get; set; }
        public string Tag { get; set; }
        public ConsumerMetadata ConsumerMetadata { get; set; }
        public string QueueName { get; set; }
        public QueueConfigTemplate ConfigTemplate { get; set; }
        public bool IsScaleInstance { get; set; }
    }
}
