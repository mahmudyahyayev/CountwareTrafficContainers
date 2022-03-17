using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public interface IQueueEventMapper : ISingletonDependency
    {
        Mhd.Framework.Queue.IQueueEvent Map(Mhd.Framework.Efcore.IDomainEvent @event, Guid userId);
        List<Mhd.Framework.Queue.IQueueEvent> MapAll(IEnumerable<Mhd.Framework.Efcore.IDomainEvent> events, Guid userId);
    }
}
