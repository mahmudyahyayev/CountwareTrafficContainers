using Sensormatic.Tool.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Core
{
    public interface IAreaRepository : IRepository<Area>, IScopedDependency
    {
        Task<Area> GetAsync(Guid id);
        Task<QueryablePagingValue<Area>> GetAllAsync(int page, int limit, Guid districtId);
        Task<bool> ExistsAsync(string name);
        Task<bool> ExistsAsync(Guid id);
    }
}
