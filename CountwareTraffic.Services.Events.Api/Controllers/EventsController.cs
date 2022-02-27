using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Api.Controllers
{
    public class EventsController : BaseController<EventsController>
    {
        private readonly ILogger<EventsController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public EventsController(IServiceProvider provider, ILogger<EventsController> logger, IHttpContextAccessor contextAccessor, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{deviceId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetEventsAsync(Guid deviceId, [FromQuery] Paging paging)
        {
            var events = await _queryDispatcher.QueryAsync(new GetEvents
            {
                DeviceId = deviceId,
                PagingQuery = new PagingQuery(paging.Page, paging.Limit)

            });

            return Ok(events);
        }


        [HttpPost("{devieId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddEventAsync(Guid devieId, [FromForm] CreateEventRequest request)
        {
            await _commandDispatcher.SendAsync(new CreateEvent
            {
                DeviceId = devieId,
                Description = request.Description,
                DirectionTypeId = request.DirectionTypeId,
                EventDate = DateTime.Now
            });

            return Ok(new { Created = "Success" });
        }       
    }
}
