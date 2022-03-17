using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mhd.Framework.MongoDb
{
    public interface IMongoDbRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(string id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(params TEntity[] entities);
        Task UpdateAsync(string id, TEntity entity);
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(string id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
