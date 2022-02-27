using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Events.Application
{
    public class GetEvents : IQuery<PagingResult<EventDetailsDto>>
    {
        public Guid DeviceId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
