using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Core
{
    public interface IDeviceRepository : IRepository<Device>, IScopedDependency
    {
        Task<Device> GetAsync(Guid id);
        Task<QueryablePagingValue<Device>> GetAllAsync(int page, int limit, Guid subAreaId);
        Task<bool> ExistsAsync(string name);
        Task<Device> GetDeletedAsync(Guid id);
    }
}
