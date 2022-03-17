using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mhd.Framework.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ServiceFilter(typeof(MonitoringFilterAction))]
    [ServiceFilter(typeof(ValidationFilterAction))]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        public BaseApiController(IServiceProvider provider, MonitoringResultHandler dispatcher)
            => provider
                .GetService<MonitoringFilterAction>()
                .OnMonitoringResultHandler += dispatcher;
    }
}
