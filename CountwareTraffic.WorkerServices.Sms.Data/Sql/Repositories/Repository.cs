using Sensormatic.Tool.Efcore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountwareTraffic.WorkerServices.Sms.Data
{
    public interface IRepository { }
    public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly SmsDbContext _context;
        public Repository(SmsDbContext context) =>
            _context = context;

        public async Task AddAsync(TEntity entity) =>
            await _context.Set<TEntity>()
                          .AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) =>
            await _context.Set<TEntity>()
                          .AddRangeAsync(entities);
    }
}
