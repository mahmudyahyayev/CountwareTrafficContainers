using Convey.CQRS.Events;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public class EventCreatedHandler : IEventHandler<EventCreated>
    {
        private readonly IEventElasticSearchRepository _eventElasticSearchRepository;
        public EventCreatedHandler(IEventElasticSearchRepository eventElasticSearchRepository)
        {
            _eventElasticSearchRepository = eventElasticSearchRepository;
        }

        public async Task HandleAsync(EventCreated e)
        {
            await _eventElasticSearchRepository.AddAsync(new EventCreateElasticDto
            {
                Description = e.Description,
                EventId = e.EventId,
                DeviceId = e.DeviceId,
                DeviceName = e.DeviceName,
                DirectionTypeId = e.DirectionTypeId,
                DirectionTypeName = e.DirectionTypeName,
                EventDate = e.EventDate,
                CreatedBy = e.CreateBy,
                CreatedDate = e.CreateDate
            });
        }
    }
}
