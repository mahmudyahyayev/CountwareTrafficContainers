using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class SubAreaRepository : Repository<SubArea>, ISubAreaRepository
    {
        private readonly new AreaDbContext _context;
        public SubAreaRepository(AreaDbContext context) : base(context) 
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

        public async Task<SubArea> GetAsync(Guid id) 
            => await base.GetQuery()
                                    .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(string name) 
            => await base.GetQuery()
                                   .AnyAsync(u => u.Name == name && u.AuditIsDeleted == false);

        public async Task<SubArea> GetDeletedAsync(Guid id)
            => await _context.SubAreas
                                   .Where(u => u.AuditIsDeleted)
                                   .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<QueryablePagingValue<SubArea>> GetAllAsync(int page, int limit, Guid areaId)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));

            var total = await query.CountAsync(x=>x.AreaId == areaId);

            if (total > 0)
            {
                var result = await query
                                    .Where(u => u.AreaId == areaId)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<SubArea>(result, total);
            }
            return null;
        }
    }
}
