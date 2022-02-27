using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sensormatic.Tool.Queue
{
    public class RabbitMQService : IQueueService
    {
        private readonly IConnection _connection;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private List<Subscriber> _subscribers { get; set; }


        public RabbitMQService(IConfiguration configuration,
            ILogger<RabbitMQService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(configuration.GetConnectionString("RabbitMQ"))
            };

            _connection = connectionFactory.CreateConnection();
            _logger = logger;
            _subscribers = new List<Subscriber>();
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Send<T>(string queueName, T data) where T : IQueueCommand
        {
            using (var channel = _connection.CreateModel())
            {
                channel.DeclareQueueWithDLX(queueName);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                var body = Encoding.UTF8.GetBytes(json);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);
            }
        }

        public void Publish<T>(T data) where T : IQueueEvent
        {
            using (var channel = _connection.CreateModel())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                var body = Encoding.UTF8.GetBytes(json);

                var exchangeName = $"event.{data.GetType().Name}";

                channel.ExchangeDeclareNoWait(exchangeName, "fanout", durable: true, arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: exchangeName,
                                     routingKey: "",
                                     basicProperties: properties,
                                     body: body);
            }
        }

        public void Subscribe<T>(string queueName, QueueConfigTemplate template) where T : IConsumer
        {
            var consumerMetadata = ConsumerInstance.GetConsumerMetadata<T>();

            Subscribe(queueName, template, consumerMetadata);
        }

        public void AutoScale()
        {
            var queueGroup = _subscribers.GroupBy(x => x.QueueName).ToList();

            using (var channel = _connection.CreateModel())
            {
                foreach (var queues in queueGroup)
                {
                    var queue = queues.First();

                    var count = channel.MessageCount(queue.QueueName);
                    var consumerCount = channel.ConsumerCount(queue.QueueName);

                    var averageConsumptionPerConsumer = 20;

                    var idealConsumerCount = Math.Ceiling((decimal)count / averageConsumptionPerConsumer);

                    var consumerMetadata = queues.First().ConsumerMetadata;

                    if (idealConsumerCount > consumerCount && consumerCount <= queue.ConfigTemplate.ScaleUpTo)
                    {
                        Subscribe(queue.QueueName, queue.ConfigTemplate, consumerMetadata, true);

                        _logger.LogInformation("Auto scaler created new instance. {Queue} {Consumer}", queue.QueueName, consumerMetadata.ConsumerType.Name);
                    }

                    else if (idealConsumerCount < consumerCount && queues.Where(x => x.IsScaleInstance).Any())
                    {
                        var cancellingConsumer = queues.Where(x => x.IsScaleInstance).FirstOrDefault();

                        try
                        {
                            cancellingConsumer.Channel.BasicCancel(cancellingConsumer.Tag);

                            Thread.Sleep(1000);

                            if (!cancellingConsumer.Channel.IsClosed)
                            {
                                cancellingConsumer.Channel.Close();
                                _logger.LogInformation("Auto scaler closed an instance. {Queue} {Consumer}", queue.QueueName, consumerMetadata.ConsumerType.Name);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Auto scaler could not close an instance. {Queue} {Consumer} ", queue.QueueName, consumerMetadata.ConsumerType.Name);

                            _subscribers.RemoveAll(x => x.Tag == queue.Tag);
                        }
                    }
                }
            }
        }

        public void StopConsumers()
        {
            var activeConsumers = _subscribers.Select(x => new { x.QueueName, Consumer = x.ConsumerMetadata.ConsumerType.Name, x.Channel, x.Tag }).ToList();

            foreach (var item in activeConsumers)
            {
                try
                {
                    item.Channel.BasicCancel(item.Tag);

                    _logger.LogInformation("Auto scaler closed an instance. {Queue} {Consumer}", item.QueueName, item.Consumer);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Auto scaler could not close an instance. {Queue} {Consumer} ", item.QueueName, item.Consumer);

                    throw;
                }
            }

            _subscribers.Clear();
        }

        private void Subscribe(string queueName, QueueConfigTemplate template, ConsumerMetadata consumerMetadata, bool isScaleInstance = false)
        {
            _logger.LogInformation("{Queue} started consuming from {Consumer}. {@Template}", queueName, consumerMetadata.ConsumerType.Name, template);

            var channel = _connection.CreateModel();

            channel.DeclareQueueWithDLX(queueName);

            if (consumerMetadata.IsEvent)
            {
                var exchangeName = $"event.{consumerMetadata.MessageType.Name}";

                channel.ExchangeDeclareNoWait(exchangeName, "fanout", durable: true, arguments: null);

                channel.QueueBind(queueName, exchangeName, "", null);
            }

            var consumer = new EventingBasicConsumer(channel);

            channel.SetDelayedDeadLetterExchange(queueName);

            consumer.Received += async (model, ea) =>
            {
                object queueModel = null;

                int retryCount = -1;

                if (int.TryParse(ea.BasicProperties.Headers?["x-retry"].ToString(), out var count))
                {
                    retryCount = count;
                }

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    queueModel = Newtonsoft.Json.JsonConvert.DeserializeObject(message, consumerMetadata.MessageType);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "{Queue} Json convert error. Message moved to fault queue", queueName);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.Headers = new Dictionary<string, object>
                    {
                        {"Error", exception.Message }
                    };

                    channel.MoveToFaultQueue(queueName, ea.DeliveryTag, body, properties);

                    return;
                }

                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var instance = scope.ServiceProvider.GetRequiredService(consumerMetadata.ConsumerType);
                        await (Task)consumerMetadata.Method.Invoke(instance, new[] { queueModel });
                    }

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "{Queue} {Consumer} Error", queueName, consumerMetadata.ConsumerType.Name);

                    channel.ExecuteRetryPolicy(queueName, template, body, ea.DeliveryTag, retryCount, ex);
                }
            };

            consumer.ConsumerCancelled += (model, ea) =>
            {
                _subscribers.RemoveAll(x => x.Tag == ea.ConsumerTags[0]);
            };

            channel.ModelShutdown += (model, ea) =>
            {
                _logger.LogWarning("{Queue} Rabbitmq model shutdown. {Reason}", queueName, ea.ReplyText);
            };

            channel.CallbackException += (model, ea) =>
            {
                _logger.LogError(ea.Exception, "{Queue} Rabbitmq callback exception occured.", queueName);
            };

            channel.BasicQos(0, template.PrefetchCount, false);

            var tag = channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

            _subscribers.Add(new Subscriber
            {
                ConfigTemplate = template,
                QueueName = queueName,
                ConsumerMetadata = consumerMetadata,
                IsScaleInstance = isScaleInstance,
                Channel = channel,
                Tag = tag
            });
        }

        public void DeleteQueue(string name)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDelete(name);
            }
        }

        public void DeleteExchange(string name)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDelete(name);
            }
        }
    }
}
