using CountwareTraffic.Services.Events.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        private readonly new EventDbContext _context;

        public DeviceRepository(EventDbContext context) : base(context) => _context = context;

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


        public async Task<Device> GetAsync(Guid id) => await base.GetQuery().SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(Guid id) => await base.GetQuery().AnyAsync(u => u.Id == id);

        public async Task<Device> GetByNameAsync(string name) => await base.GetQuery().SingleOrDefaultAsync(u => u.Name == name);

        public async Task<IEnumerable<Device>> GetAsync() => await base.GetQuery().ToListAsync();
    }
}
