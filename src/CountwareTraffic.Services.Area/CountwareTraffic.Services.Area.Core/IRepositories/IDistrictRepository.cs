using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Core
{
    public interface IDistrictRepository : IRepository<District>, IScopedDependency
    {
        Task<District> GetAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<QueryablePagingValue<District>> GetAllAsync(int page, int limit, Guid cityId);
        Task<bool> ExistsAsync(Guid id);
    }
}
