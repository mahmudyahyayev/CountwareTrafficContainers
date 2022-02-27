using Sensormatic.Tool.Ioc;
using System;

namespace CountwareTraffic.WorkerServices.Sms.Infrastructure
{
    public interface ISmsRepository : IRepository<Sms>, IScopedDependency
    {
    }
    public class SmsRepository : Repository<Sms>, ISmsRepository
    {
        private readonly new SmsDbContext _context;

        public SmsRepository(SmsDbContext context) : base(context)
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
    }
}
