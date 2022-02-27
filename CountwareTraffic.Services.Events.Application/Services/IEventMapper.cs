using Sensormatic.Tool.Ioc;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IEventMapper : ISingletonDependency
    {
        Convey.CQRS.Events.IEvent Map(Sensormatic.Tool.Queue.IQueueEvent @event);
        IEnumerable<Convey.CQRS.Events.IEvent> MapAll(IEnumerable<Sensormatic.Tool.Queue.IQueueEvent> events);
    }
}
