using Sensormatic.Tool.Ioc;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public interface IQueueEventMapper : ISingletonDependency
    {
        Sensormatic.Tool.Queue.IQueueEvent Map(Sensormatic.Tool.Efcore.IDomainEvent @event, Guid userId);
        List<Sensormatic.Tool.Queue.IQueueEvent> MapAll(IEnumerable<Sensormatic.Tool.Efcore.IDomainEvent> events, Guid userId);
    }
}
