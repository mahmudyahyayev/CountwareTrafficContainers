using Microsoft.Extensions.Configuration;
using Nest;
using Sensormatic.Tool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensormatic.Tool.ElasticSearch
{
    public abstract class ElasticRepository<T> : IElasticRepository<T>  where T : ElasticSearchEntity
    {
        protected readonly ElasticClient _client;
        protected readonly string _index;

        public ElasticRepository(IConfiguration configuration, string index)
        {
            var node = new Uri(configuration.GetConnectionString("Elastic"));
            var settings = new ConnectionSettings(node);
            settings.RequestTimeout(TimeSpan.FromMinutes(1));
            //NOT: Mahmud Yahyayev Eger lazimsa query to json yapmak icin acip kapata biliriz testlerde.  ACIP TEST ETTIKTEN SONRA KAPATMAYANIN PLAVINI KASIKLASINLAR :)
            //settings.DisableDirectStreaming();  
            _client = new ElasticClient(settings);
            _index = index;
        }

        protected bool CreateIndex(string indexName)
        {
            if (_client.Indices.Exists(indexName).Exists) return false;
            {
                return _client.Indices.Create(indexName, index => index
                    .Map<T>(x => x.AutoMap())).IsValid;
            }
        }

        public bool Add(T item)
        {
            var response = _client.Index<T>(item, idx => idx.Index(_index));
            return response.IsValid;
        }

        public async Task AddAsync(T item)
        {
            var response = await _client.IndexAsync<T>(item, idx => idx.Index(_index));

            if (!response.IsValid)
                throw response.OriginalException;
        }

        public bool AddRange(List<T> items)
        {
            var response = _client.IndexMany(items, _index);
            if (!response.IsValid)
                throw response.OriginalException;

            return response.IsValid;
        }

        public async Task<bool> AddRangeAsync(List<T> items)
        {
            var response = await _client.IndexManyAsync(items, _index);
            if (!response.IsValid)
                throw response.OriginalException;

            return response.IsValid;
        }

        public T GetById(string id)
        {
            var response = _client.Get(new DocumentPath<T>(id), idx => idx.Index(_index));
            return response.Source;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync(new DocumentPath<T>(id), idx => idx.Index(_index));
            return response.Source;
        }

        public bool Update(T item)
        {
            var response = _client.Update<T>(item, u => u.Index(_index).Doc(item).RetryOnConflict(3));
            return response.IsValid;
        }

        public async Task UpdateAsync(T item)
        {
            var response = await _client.UpdateAsync<T>(item, u => u.Index(_index).Doc(item).RetryOnConflict(3));

            if (response.ApiCall.HttpStatusCode == 404)
                throw new ResourceNotFoundException(_index, item.Id.ToString());

            if (!response.IsValid)
                throw response.OriginalException;
        }

        public bool UpdateByQuery(QueryContainer queryContainer, string updateQuery)
        {
            Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> updateRequest = updateDescriptor => updateDescriptor
                .Index(_index)
                .Script(updateQuery)
                .Query(q => queryContainer);

            var response = _client.UpdateByQuery<T>(updateRequest);
            return response.IsValid;
        }

        public async Task<bool> UpdateByQueryAsync(QueryContainer queryContainer, string updateQuery)
        {
            Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> updateRequest = updateDescriptor => updateDescriptor
                .Index(_index)
                .Script(updateQuery)
                .Query(q => queryContainer);

            var response = await _client.UpdateByQueryAsync<T>(updateRequest);
            return response.IsValid;
        }

        public bool Delete(string id)
        {
            var response = _client.Delete<T>(new DocumentPath<T>(id), idx => idx.Index(_index));
            return response.IsValid;
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync<T>(new DocumentPath<T>(id), idx => idx.Index(_index));

            if (response.ApiCall.HttpStatusCode == 404)
                throw new ResourceNotFoundException(_index, id);

            if (!response.IsValid)
                throw response.OriginalException;
        }

        public bool DeleteByQuery(QueryContainer queryContainer)
        {
            Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> deleteRequest = deleteDescriptor => deleteDescriptor
                .Index(_index)
                .Query(q => queryContainer);

            var response = _client.DeleteByQuery<T>(deleteRequest);
            return response.IsValid;
        }

        public async Task<bool> DeleteByQueryAsync(QueryContainer queryContainer)
        {
            Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> deleteRequest = deleteDescriptor => deleteDescriptor
                .Index(_index)
                .Query(q => queryContainer);

            var response = await _client.DeleteByQueryAsync<T>(deleteRequest);
            return response.IsValid;
        }

        public async Task<IList<T>> FilterAsync(List<Func<QueryContainerDescriptor<T>, QueryContainer>> filters)
        {
            var response = await _client.SearchAsync<T>(x => x.Index(_index).Query(q => q.Bool(bq => bq.Filter(filters.ToArray()))));
            return response.Documents.ToList();
        }

        public async Task<IEnumerable<T>> FindAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int size = ElasticsearchKeys.DefaultSize)
        {
            var response = await _client.SearchAsync<T>(x => x
                .Index(_index)
                .From(ElasticsearchKeys.From)
                .Size(size)
                .Query(query));

            return response.Documents;
        }

        public async Task<T> FindOneAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query)
        {
            var response = await _client.SearchAsync<T>(x => x
                .Index(_index)
                .From(ElasticsearchKeys.From)
                .Size(ElasticsearchKeys.MinSize)
                .Query(query));

            return response.Documents.FirstOrDefault();
        }

        public async Task<List<T>> SearchPagingAsync(QueryContainerSearch request)
        {
            Func<SearchDescriptor<T>, ISearchRequest> searchRequest = searchDescriptor => searchDescriptor
                .From(request.From)
                .Size(request.Size)
                .Index(_index)
                .Query(q => request.QueryContainer)
                .Sort(s => string.IsNullOrEmpty(request.SortField) ? s : s.Field(fs => fs.Field(request.SortField).Order(request.Direction)));

            CreateIndex(_index);
            var response = await _client.SearchAsync<T>(searchRequest);

            return response.Documents.ToList();
        }

        public List<T> SearchPaging(QueryContainerSearch request)
        {
            Func<SearchDescriptor<T>, ISearchRequest> searchRequest = searchDescriptor => searchDescriptor
                .From(request.From)
                .Size(request.Size)
                .Index(_index)
                .Query(q => request.QueryContainer)
                .Sort(s => string.IsNullOrEmpty(request.SortField) ? s : s.Field(fs => fs.Field(request.SortField).Order(request.Direction)));

            var result = _client.Search<T>(searchRequest);

            if (!result.IsValid) throw new Exception(result.OriginalException.Message);

            return result.Documents.ToList();
        }

        public async Task<int> SearchCountAsync(QueryContainerSearch request)
        {
            Func<CountDescriptor<T>, ICountRequest> countRequest = countDescriptor => countDescriptor
                  .Index(_index)
                  .Query(q => request.QueryContainer);

            var result = await _client.CountAsync(countRequest);

            if (!result.IsValid) throw new Exception(result.OriginalException.Message);

            return (int)result.Count;
        }

        public int SearchCount(QueryContainerSearch request)
        {
            Func<CountDescriptor<T>, ICountRequest> countRequest = countDescriptor => countDescriptor
                .Index(_index)
                .Query(q => request.QueryContainer);

            var result = _client.Count(countRequest);

            if (!result.IsValid) throw new Exception(result.OriginalException.Message);

            return (int)result.Count;
        }
    }
}
