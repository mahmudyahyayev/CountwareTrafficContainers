using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensormatic.Tool.ElasticSearch
{
    public interface IElasticRepository<T> where T : class
    {
        bool Add(T item);
        Task AddAsync(T item);
        bool AddRange(List<T> items);
        Task<bool> AddRangeAsync(List<T> items);
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        bool Update(T item);
        Task UpdateAsync(T item);
        bool UpdateByQuery(QueryContainer queryContainer, string updateQuery);
        Task<bool> UpdateByQueryAsync(QueryContainer queryContainer, string updateQuery);
        bool Delete(string id);
        Task DeleteAsync(string id);
        bool DeleteByQuery(QueryContainer queryContainer);
        Task<bool> DeleteByQueryAsync(QueryContainer queryContainer);
        Task<IList<T>> FilterAsync(List<Func<QueryContainerDescriptor<T>, QueryContainer>> filters);
        Task<T> FindOneAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query);
        Task<IEnumerable<T>> FindAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int size = 10);
    }
}
