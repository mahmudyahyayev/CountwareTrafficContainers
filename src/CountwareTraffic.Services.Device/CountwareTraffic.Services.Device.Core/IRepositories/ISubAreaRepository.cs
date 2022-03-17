using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Core
{
    public interface ISubAreaRepository : IRepository<SubArea>, IScopedDependency
    {
        Task<SubArea> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
