using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        private readonly new DeviceDbContext _context;

        public DeviceRepository(DeviceDbContext context) : base(context)
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


        public async Task<Device> GetAsync(Guid id)
            => await base.GetQuery()
                                    .Include(x => x.ConnectionInfo)
                                    .Include(x => x.DeviceStatus)
                                    .Include(x => x.DeviceType)
                                    .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Device> GetDeletedAsync(Guid id)
             => await _context.Devices
                                    .Where(u => u.AuditIsDeleted)
                                    .Include(x => x.ConnectionInfo)
                                    .Include(x => x.DeviceStatus)
                                    .Include(x => x.DeviceType)
                                    .SingleOrDefaultAsync(x => x.Id == id);


        public async Task<bool> ExistsAsync(string name)
            => await base.GetQuery()
                                   .AnyAsync(u => u.Name == name);


        public async Task<QueryablePagingValue<Device>> GetAllAsync(int page, int limit, Guid subAreaId)
        {
            var query = base.GetQuery(orderBy: x => x.OrderBy(y => y.Id));
            var total = await query.CountAsync(x => x.SubAreaId == subAreaId);

            if (total > 0)
            {
                if (subAreaId != Guid.Empty)
                    query = query.Where(u => u.SubAreaId == subAreaId);

                var result = await query
                                    .Include(x => x.ConnectionInfo)
                                    .Include(x => x.DeviceStatus)
                                    .Include(x => x.DeviceType)
                                    .Skip((page - 1) * limit)
                                    .Take(limit)
                                    .ToListAsync();

                return new QueryablePagingValue<Device>(result, total);
            }
            return null;
        }
    }
}
