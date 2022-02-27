using CountwareTraffic.Services.Companies.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        private readonly new AreaDbContext _context;

        public DistrictRepository(AreaDbContext context) : base(context) 
            => _context = context;

        #region disposible
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion disposible

        public async Task<bool> ExistsAsync(string name)
            => await base.GetQuery()
                                    .AnyAsync(u => u.Name == name);

        public async Task<District> GetAsync(Guid id)
            => await base.GetQuery()
                                    .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(Guid id)
          => await base.GetQuery()
                                 .AnyAsync(u => u.Id == id);


        public async Task<QueryablePagingValue<District>> GetAllAsync(int page, int limit, Guid cityId)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));

            var total = await query.CountAsync();

            if (total > 0)
            {
                var result = await query
                                    .Where(u=>u.CityId ==cityId)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<District>(result, total);
            }
            return null;
        }
    }
}
