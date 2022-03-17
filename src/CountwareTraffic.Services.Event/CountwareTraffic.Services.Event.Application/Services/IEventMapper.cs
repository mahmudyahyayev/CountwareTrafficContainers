using Mhd.Framework.Ioc;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IEventMapper : ISingletonDependency
    {
        Convey.CQRS.Events.IEvent Map(Mhd.Framework.Queue.IQueueEvent @event);
        IEnumerable<Convey.CQRS.Events.IEvent> MapAll(IEnumerable<Mhd.Framework.Queue.IQueueEvent> events);
    }
}
