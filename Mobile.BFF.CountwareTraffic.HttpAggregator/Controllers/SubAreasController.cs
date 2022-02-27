using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Api;
using Sensormatic.Tool.Common;
using Sensormatic.Tool.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers
{
    [Authorize]
    public class SubAreasController : BaseController<SubAreasController>
    {
        private readonly ILogger<SubAreasController> _logger;
        private readonly SubAreaService _subAreaService;
        public SubAreasController(IServiceProvider provider, ILogger<SubAreasController> logger, IHttpContextAccessor contextAccessor, SubAreaService subAreaService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _subAreaService = subAreaService;
        }

        [HttpGet("{subAreaId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetSubAreaDetailAsync(Guid subAreaId)
        {
            var subArea = await _subAreaService.GetSubAreaByIdAsync(subAreaId);

            return Ok(subArea);
        }

        [HttpGet("{areaId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetSubAreaDetailsAsync(Guid areaId, [FromQuery] Paging paging)
        {
            var subAreas = await _subAreaService.GetSubAreasAsync(areaId, paging);

            return Ok(subAreas);
        }

        [HttpPost("{areaId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddSubAreaAsync(Guid areaId, [FromForm] AddSubAreaRequest request)
        {
            await _subAreaService.AddSubAreaAsync(areaId, request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{subAreaId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeSubAreaAsync(Guid subAreaId, [FromForm] ChangeSubAreaRequest request)
        {
            await _subAreaService.ChangeSubAreaAsync(subAreaId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{subAreaId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteSubAreaAsync(Guid subAreaId)
        {
            await _subAreaService.DeleteSubAreaAsync(subAreaId);

            return Ok(new { Deleted = "Success" });
        }
    }
}
