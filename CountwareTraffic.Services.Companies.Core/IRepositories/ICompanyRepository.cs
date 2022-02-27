using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Core
{
    public interface ICompanyRepository : IRepository<Company>, IScopedDependency
    {
        Task<Company> GetAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<QueryablePagingValue<Company>> GetAllAsync(int page, int limit);
        Task<bool> ExistsAsync(Guid id);
    }
}
