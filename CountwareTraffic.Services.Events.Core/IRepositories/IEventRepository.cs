using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Core
{
    public interface IEventRepository : IRepository<Event>, IScopedDependency
    {
        Task<Event> GetAsync(Guid id);
        Task<QueryablePagingValue<Event>> GetAllAsync(int page, int limit, Guid deviceId);
    }
}
