using CountwareTraffic.Services.Devices.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure.Repositories
{
    public class SubAreaRepository : Repository<SubArea>, ISubAreaRepository
    {
        private bool _disposed;

        private readonly new DeviceDbContext _context;

        public SubAreaRepository(DeviceDbContext context) : base(context)
            => _context = context;

        #region dispose
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


        public async Task<SubArea> GetAsync(Guid id)
            => await _context.SubAreas
                                     .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _context.SubAreas.AnyAsync(u => u.Id == id);
    }
}
