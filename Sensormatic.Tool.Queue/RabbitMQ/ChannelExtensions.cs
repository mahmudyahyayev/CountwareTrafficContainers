using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace Sensormatic.Tool.Queue
{
    public static class ChannelExtensions
    {
        public static void SetDelayedDeadLetterExchange(this IModel channel, string queueName)
        {
            var dlxExchange = $"dlx.{queueName}";

            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("x-delayed-type", "fanout");

            channel.ExchangeDeclare(dlxExchange, "x-delayed-message", arguments: args);
            channel.QueueBind(queueName, dlxExchange, "", null);
        }

        public static void MoveToFaultQueue(this IModel channel, string queueName, ulong deliveryTag, byte[] body, IBasicProperties properties)
        {
            var faultQueue = $"{queueName}_Fault";

            channel.BasicAck(deliveryTag, false);

            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("x-queue-mode", "lazy");

            channel.QueueDeclareNoWait(queue: faultQueue,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: args);

            channel.BasicPublish(exchange: "",
                         routingKey: faultQueue,
                         basicProperties: properties,
                         body: body);
        }

        public static void ExecuteRetryPolicy(this IModel channel, string queueName, QueueConfigTemplate template, byte[] body, ulong deliveryTag, int retryCount, Exception exception)
        {
            var dlxExchange = $"dlx.{queueName}";

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.Headers = new Dictionary<string, object>
            {
                {"x-delay", TimeSpan.FromSeconds(template.RetryIntervalSeconds).TotalMilliseconds.ToString() },
                {"x-retry", retryCount + 1 }
            };

            if (template.ExcludeExceptions.Contains(exception.GetType()) || retryCount >= template.RetryCount)
            {
                properties.Headers.Add("Error", exception.Message);

                channel.MoveToFaultQueue(queueName, deliveryTag, body, properties);
            }
            else
            {
                channel.BasicAck(deliveryTag, false);

                channel.BasicPublish(exchange: dlxExchange,
                             routingKey: "",
                             basicProperties: properties,
                             body: body);
            }
        }

        public static void DeclareQueueWithDLX(this IModel channel, string queueName)
        {
            var exchange = $"dlx.{queueName}";

            Dictionary<string, Object> args = new Dictionary<string, Object>();
            args.Add("x-dead-letter-exchange", exchange);

            channel.QueueDeclareNoWait(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: args);
        }
    }
}
