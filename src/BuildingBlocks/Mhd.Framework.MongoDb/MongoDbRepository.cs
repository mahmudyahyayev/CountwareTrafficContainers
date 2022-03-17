using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mhd.Framework.MongoDb
{
    public abstract class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : MongoDbEntity
    {
        protected readonly IMongoCollection<TEntity> Collection;
        protected MongoDbRepository(IConfiguration configuration, string databaseName, string collectionName)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(databaseName);
            Collection = database.GetCollection<TEntity>(collectionName);
        }
        public async Task AddAsync(TEntity entity) => await Collection.InsertOneAsync(entity);
        public async Task AddRangeAsync(params TEntity[] entities) => await Collection.InsertManyAsync(entities);
        public async Task DeleteAsync(string id) => await Collection.DeleteOneAsync(x => x.Id == id);
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate) => await Collection.DeleteOneAsync(predicate);
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) => Collection.Find(predicate).FirstOrDefaultAsync();
        public async Task<TEntity> GetByIdAsync(string id) => await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task UpdateAsync(string id, TEntity entity) => await Collection.ReplaceOneAsync(x => x.Id == id, entity);
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)=> await Collection.ReplaceOneAsync(predicate, entity);
    }
}
