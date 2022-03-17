using Microsoft.Extensions.DependencyInjection;
using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.Email.Data
{
    public interface IUnitOfWork : IScopedDependency
    {
        T GetRepository<T>() where T : IRepository;
        int Commit();
        Task<int> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EmailDbContext _context;

        public UnitOfWork(EmailDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public T GetRepository<T>() where T : IRepository
            => _serviceProvider.GetService<T>();

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
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
