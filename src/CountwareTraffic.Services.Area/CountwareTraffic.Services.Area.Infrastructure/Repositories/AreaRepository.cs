using CountwareTraffic.Services.Areas.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        private readonly new AreaDbContext _context;

        public AreaRepository(AreaDbContext context) : base(context) 
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

        public async Task<Area> GetAsync(Guid id)
            => await base.GetQuery()
                                    .Include(x => x.Address)
                                    .Include(x => x.Contact)
                                    .Include(x => x.AreaType)
                                    .SingleOrDefaultAsync(x => x.Id == id);
        
        public async Task<bool> ExistsAsync(string name)
            => await base.GetQuery()
                                   .AnyAsync(u => u.Name == name);

        public async Task<bool> ExistsAsync(Guid id)
           => await base.GetQuery()
                                  .AnyAsync(u => u.Id == id);

        public async Task<QueryablePagingValue<Area>> GetAllAsync(int page, int limit, Guid districtId)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));

            #region denemeler
            //Net core 3.1 ve sonrasi calismaz. Gereksiz yere gorup by yapmis oluruz.
            //var grouppedAreas = await query
            //                            .Include(x => x.Address)
            //                            .Include(x=>x.AreaType)
            //                            .Include(x => x.Contact)
            //                            .OrderBy(x => x.Id)
            //                            .Select(x => x)
            //                            .Skip((page - 1) * limit)
            //                            .Take(limit)
            //                            .GroupBy(x => new { Total = query.Count() })
            //                            .FirstOrDefaultAsync();

            //var tsqlQueryControl = query
            //                    .Include(x => x.Address)
            //                    .Include(x => x.AreaType)
            //                    .Include(x => x.Contact)
            //                    .OrderBy(x => x.Id)
            //                    .Select(x => new { Data = x, Total = query.Count() })
            //                    .Skip((page - 1) * limit)
            //                    .Take(limit);


            //string tsqlQuery = tsqlQueryControl.ToQueryString();


            //Buda kotu bir yontem her seferde sayar.
            //var result = await query
            //                        .Include(x => x.Address)
            //                        .Include(x => x.AreaType)
            //                        .Include(x => x.Contact)
            //                        .OrderBy(x => x.Id)
            //                        .Select(x => new { Data = x, Total = query.Count() })
            //                        .Skip((page - 1) * limit)
            //                        .Take(limit)
            //                        .ToListAsync();

            //.Net 5 EfCore 5 versiyonulari icin en optimize yontem iki defa veri tabanina gitmek gibi.
            #endregion

            var total = await query.CountAsync(x=>x.DistrictId == districtId);

            if (total > 0)
            {
                var result = await query
                                    .Where(u => u.DistrictId == districtId)
                                    .Include(x => x.Address)
                                    .Include(x => x.AreaType)
                                    .Include(x => x.Contact)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<Area>(result, total);
            }
            return null;
        }
    }
}
