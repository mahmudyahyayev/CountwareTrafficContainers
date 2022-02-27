using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Core
{
    public interface ICountryRepository : IRepository<Country>, IScopedDependency
    {
        Task<Country> GetAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<QueryablePagingValue<Country>> GetAllAsync(int page, int limit, Guid companyId);
        Task<bool> ExistsAsync(Guid id);
    }
}
