using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace CountwareTraffic.Services.Areas.Api.Controllers
{
    [Authorize]
    public class DistrictsController : BaseController<DistrictsController>
    {
        private readonly ILogger<DistrictsController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public DistrictsController(IServiceProvider provider, ILogger<DistrictsController> logger, IHttpContextAccessor contextAccessor, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
    }
}
