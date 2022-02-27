using CountwareTraffic.Services.Companies.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly new AreaDbContext _context;

        public CityRepository(AreaDbContext context) : base(context) 
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

        public async Task<City> GetAsync(Guid id)
            => await base.GetQuery()
                                    .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(Guid id)
            => await base.GetQuery()
                                   .AnyAsync(u => u.Id == id);

        public async Task<QueryablePagingValue<City>> GetAllAsync(int page, int limit, Guid countryId)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));

            var total = await query.CountAsync();

            if (total > 0)
            {
                var result = await query
                                    .Where(u=>u.CountryId == countryId)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<City>(result, total);
            }
            return null;
        }
    }
}
