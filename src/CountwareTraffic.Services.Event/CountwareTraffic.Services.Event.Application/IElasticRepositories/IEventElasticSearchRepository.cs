using CountwareTraffic.Services.Events.Core;
using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public interface IEventElasticSearchRepository : IScopedDependency 
    {
        Task AddAsync(EventCreateElasticDto data);
        Task<QueryablePagingValue<EventDetailsDto>> GetEventsAsync(Guid deviceId, int page, int size);
    }
}
