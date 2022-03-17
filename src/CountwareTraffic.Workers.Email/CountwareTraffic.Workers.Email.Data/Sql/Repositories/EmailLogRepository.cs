using Mhd.Framework.Ioc;
using System;

namespace CountwareTraffic.Workers.Email.Data
{
    public interface IEmailLogRepository : IRepository<EmailLog>, IScopedDependency { }

    public class EmailLogRepository : Repository<EmailLog>, IEmailLogRepository
    {
        private readonly new EmailDbContext _context;

        public EmailLogRepository(EmailDbContext context) : base(context) => _context = context;

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
