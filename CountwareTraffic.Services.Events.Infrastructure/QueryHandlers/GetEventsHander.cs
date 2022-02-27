using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class GetEventsHander : IQueryHandler<GetEvents, PagingResult<EventDetailsDto>>
    {
        private readonly IEventElasticSearchRepository _eventElasticSearchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetEventsHander> _logger;
        public GetEventsHander(IEventElasticSearchRepository eventElasticSearchRepository, IUnitOfWork unitOfWork, ILogger<GetEventsHander> logger)
        {
            _eventElasticSearchRepository = eventElasticSearchRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<PagingResult<EventDetailsDto>> HandleAsync(GetEvents query)
        {
            int page = query.PagingQuery.Page;
            int size = query.PagingQuery.Limit;

            QueryablePagingValue<EventDetailsDto> qres;

            try
            {
                qres = await FromElasticAsync(query.DeviceId, page, size);
            }
            catch (ElasticIndexNotFoundException ex)
            {
                _logger.LogCritical($"Message: {ex.Message}  IndexName: {ex.IndexName}");
                qres = await FromDbAsync(query.DeviceId, page, size);
            }
            catch (ElasticSearchQueryException ex)
            {
                _logger.LogCritical($"Message: {ex.Message}  IndexName: {ex.IndexName}");
                qres = await FromDbAsync(query.DeviceId, page, size);
            }

            if (qres == null)
                return PagingResult<EventDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (size * (page - 1)) + qres.Entities.Count;

            return new PagingResult<EventDetailsDto>(qres.Entities, qres.Total, page, size, hasNextPage);
        }


        //FromElastic
        private async Task<QueryablePagingValue<EventDetailsDto>> FromElasticAsync(Guid deviceId, int page, int size)
        {
            return await _eventElasticSearchRepository
                                                     .GetEventsAsync(deviceId, page, size);
        }


        //FromDb
        private async Task<QueryablePagingValue<EventDetailsDto>> FromDbAsync(Guid deviceId, int page, int limit)
        {
            var result = await _unitOfWork
                                          .GetRepository<IEventRepository>()
                                          .GetAllAsync(page, limit, deviceId);

            if (result == null) return null;

            return new QueryablePagingValue<EventDetailsDto>(result.Entities.Select(e => new EventDetailsDto
            {
                AuditCreateBy = e.CreatedBy,
                AuditCreateDate = e.CreateDate,
                Description = e.Description,
                DeviceId = e.DeviceId,
                Id = e.Id,
                DeviceName = "",
                DirectionTypeId = e.DirectionType.Id,
                DirectionTypeName = e.DirectionType.Name,
                EventDate = e.EventDate
            }).ToList(), result.Total);
        }
    }
}
