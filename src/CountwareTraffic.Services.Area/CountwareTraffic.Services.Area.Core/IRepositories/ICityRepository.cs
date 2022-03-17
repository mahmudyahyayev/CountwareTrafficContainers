using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Core
{
    public interface ICityRepository : IRepository<City>, IScopedDependency
    {
        Task<City> GetAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<bool> ExistsAsync(Guid id);
        Task<QueryablePagingValue<City>> GetAllAsync(int page, int limit, Guid countryId);
    }
}
