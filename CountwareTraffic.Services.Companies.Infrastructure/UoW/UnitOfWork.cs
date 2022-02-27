using CountwareTraffic.Services.Companies.Core;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AreaDbContext _context;
        private readonly IOutboxIDomainEventsDispatcher _outboxIDomainEventsDispatcher;

        public UnitOfWork(AreaDbContext context, IServiceProvider serviceProvider, IOutboxIDomainEventsDispatcher outboxIDomainEventsDispatcher)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _outboxIDomainEventsDispatcher = outboxIDomainEventsDispatcher;
        }

        public T GetRepository<T>() where T : IRepository => _serviceProvider.GetService<T>();

        public int Commit()
        {
            _outboxIDomainEventsDispatcher.DispatchEventsAsync().GetAwaiter().GetResult();
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            await _outboxIDomainEventsDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync();
        }

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
