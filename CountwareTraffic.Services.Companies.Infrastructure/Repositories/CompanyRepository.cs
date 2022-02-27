using CountwareTraffic.Services.Companies.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly new AreaDbContext _context;

        public CompanyRepository(AreaDbContext context) : base(context) 
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


        public async Task<Company> GetAsync(Guid id)
            => await base.GetQuery()
                                   .Include(x => x.Address)
                                   .Include(x => x.Contact)
                                   .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(string name)
            => await base.GetQuery()
                                   .AnyAsync(u => u.Name == name);

        public async Task<bool> ExistsAsync(Guid id)
          => await base.GetQuery()
                                 .AnyAsync(u => u.Id == id);

        public async Task<QueryablePagingValue<Company>> GetAllAsync(int page, int limit)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));

            var total = await query.CountAsync();

            if (total > 0)
            {
                var result = await query
                                    .Include(x => x.Address)
                                    .Include(x => x.Contact)
                                    .OrderBy(x => x.Id)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<Company>(result, total);
            }
            return null;
        }
    }
}
