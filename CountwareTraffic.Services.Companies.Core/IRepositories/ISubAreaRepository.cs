using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Core
{
    public interface ISubAreaRepository : IRepository<SubArea> , IScopedDependency
    {
        Task<SubArea> GetAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<QueryablePagingValue<SubArea>> GetAllAsync(int page, int limit, Guid areaId);
        Task<SubArea> GetDeletedAsync(Guid id);
    }
}
