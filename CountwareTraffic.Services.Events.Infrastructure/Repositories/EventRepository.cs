using CountwareTraffic.Services.Events.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly new EventDbContext _context;

        public EventRepository(EventDbContext context) : base(context)
        {
            _context = context;
        }

        #region dispose
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
        #endregion dispose

        public async Task<Event> GetAsync(Guid id)
            => await base.GetQuery()
                                    .Include(x => x.DirectionType)
                                    .SingleOrDefaultAsync(x => x.Id == id);


        public async Task<QueryablePagingValue<Event>> GetAllAsync(int page, int limit, Guid deviceId)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));
            var total = await query.CountAsync();

            if (total > 0)
            {
                if (deviceId != Guid.Empty)
                    query = query.Where(u => u.DeviceId == deviceId);

                var result = await query
                                    .Include(x => x.DirectionType)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<Event>(result, total);
            }
            return null;
        }
    }
}
