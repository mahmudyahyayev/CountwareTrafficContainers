using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Application
{
    public interface IQueueEventMapper : IScopedDependency
    {
        Mhd.Framework.Queue.IQueueEvent Map(Mhd.Framework.Efcore.IDomainEvent @event, Guid userId);
        List<Mhd.Framework.Queue.IQueueEvent> MapAll(IEnumerable<Mhd.Framework.Efcore.IDomainEvent> events, Guid userId);
    }
}
