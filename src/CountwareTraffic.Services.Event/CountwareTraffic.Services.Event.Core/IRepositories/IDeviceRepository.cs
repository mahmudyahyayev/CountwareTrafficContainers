using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Core
{
    public interface IDeviceRepository : IRepository<Device>, IScopedDependency
    {
        Task<Device> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<Device> GetByNameAsync(string name);
        Task<IEnumerable<Device>> GetAsync();
    }
}