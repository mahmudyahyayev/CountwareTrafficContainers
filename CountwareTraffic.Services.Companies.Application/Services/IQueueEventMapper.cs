using Sensormatic.Tool.Ioc;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Application
{
    public interface IQueueEventMapper : IScopedDependency
    {
        Sensormatic.Tool.Queue.IQueueEvent Map(Sensormatic.Tool.Efcore.IDomainEvent @event, Guid userId);
        List<Sensormatic.Tool.Queue.IQueueEvent> MapAll(IEnumerable<Sensormatic.Tool.Efcore.IDomainEvent> events, Guid userId);
    }
}
