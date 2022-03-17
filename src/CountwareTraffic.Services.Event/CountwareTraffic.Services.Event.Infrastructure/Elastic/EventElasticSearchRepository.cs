using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Core;
using Microsoft.Extensions.Configuration;
using Nest;
using Mhd.Framework.Common;
using Mhd.Framework.ElasticSearch;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventElasticSearchRepository : ElasticRepository<EventCreateElasticData>, IEventElasticSearchRepository
    {
        public EventElasticSearchRepository(IConfiguration configuration) 
            : base(configuration, ElasticsearchKeys.CountwareTrafficEventsEvent) { }

        #region disposible
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private async Task MapAsync()
        {
            if ((await _client.Indices.ExistsAsync(_index)).Exists)
                return;

            var createIndexResponse = await _client.Indices.CreateAsync(_index, c => c
                .Map<EventCreateElasticData>(m => m
                    .AutoMap()
                    ));

            if (!createIndexResponse.IsValid)
                throw createIndexResponse.OriginalException;
        }

        public async Task AddAsync(EventCreateElasticDto data)
        {
            await MapAsync();

            await base.AddAsync(new EventCreateElasticData
            {
                Description = data.Description,
                DeviceId = data.DeviceId,
                DeviceName = data.DeviceName,
                DirectionTypeId = data.DirectionTypeId,
                DirectionTypeName = data.DirectionTypeName,
                EventDate = data.EventDate,
                EventId = data.EventId,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate
            });
        }

        public async Task<QueryablePagingValue<EventDetailsDto>> GetEventsAsync(Guid deviceId, int page, int size)
        {
            if (!(await _client.Indices.ExistsAsync(_index)).Exists)
                throw new ElasticIndexNotFoundException(_index);

            var descriptor = new QueryContainerDescriptor<EventCreateElasticData>();

            if (deviceId != Guid.Empty)
                descriptor.Bool(b => b.Must(m => m.Term(t => t.Field(f => f.DeviceId).Value(deviceId))));


            var totalCount = await _client.CountAsync<EventCreateElasticData>(search => search
                                                                                              .Index(_index)
                                                                                              .Query(d => descriptor));

            if (!totalCount.IsValid)
                throw new ElasticSearchQueryException(_index, totalCount.ServerError.Status, totalCount.ServerError.Error.Type, totalCount.OriginalException);

            if (totalCount.Count < 1)
                return null;

            var total = (int)totalCount.Count;

            var result = await _client.SearchAsync<EventCreateElasticData>(
               search => search
               .Index(_index)
               .Size(size)
               .From((page - 1) * size)
               .Source(sf => sf
                    .Includes(i => i
                        .Fields(f => f.Description,
                                f => f.DeviceId,
                                f => f.DeviceName,
                                f => f.DirectionTypeId,
                                f => f.DirectionTypeName,
                                f => f.EventDate,
                                f => f.EventId,
                                f => f.CreatedBy,
                                f => f.CreatedDate
                              )
                           )
                      )
               .Query(d => descriptor));

            if (!result.IsValid)
                throw new ElasticSearchQueryException(_index, result.ServerError.Status, result.ServerError.Error.Type, result.OriginalException);


            var events = result.Documents.Select(x => new EventDetailsDto
            {
                AuditCreateBy = x.CreatedBy,
                DeviceName = x.DeviceName,
                AuditCreateDate = x.CreatedDate,
                Description = x.Description,
                DeviceId = x.DeviceId,
                DirectionTypeId = x.DirectionTypeId,
                DirectionTypeName = x.DirectionTypeName,
                EventDate = x.EventDate,
                Id = x.EventId
            }).ToList();

            return new QueryablePagingValue<EventDetailsDto>(events, total);
        }
    }
}
