using Mhd.Framework.Ioc;
using System;

namespace Mhd.Framework.Queue
{
    public interface IQueueService : ISingletonDependency
    {
        void Send<T>(string queueName, T data) where T : IQueueCommand;
        void Publish<T>(T data) where T : IQueueEvent;
        void Subscribe<T>(string queueName, QueueConfigTemplate template) where T : IConsumer;
        void AutoScale();
        void StopConsumers();
        void DeleteQueue(string name);
        void DeleteExchange(string name);
    }
}
