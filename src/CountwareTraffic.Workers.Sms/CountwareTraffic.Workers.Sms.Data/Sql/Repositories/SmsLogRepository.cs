using Mhd.Framework.Ioc;
using System;

namespace CountwareTraffic.Workers.Sms.Data
{
    public interface ISmsLogRepository : IRepository<SmsLog>, IScopedDependency { }

    public class SmsLogRepository : Repository<SmsLog>, ISmsLogRepository
    {
        private readonly new SmsDbContext _context;

        public SmsLogRepository(SmsDbContext context) : base(context) => _context = context;

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
    }
}
