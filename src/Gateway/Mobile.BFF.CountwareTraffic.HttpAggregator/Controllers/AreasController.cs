using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Api;
using Mhd.Framework.Core;
using System;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers
{
    [Authorize]
    public class AreasController : BaseController<AreasController>
    {
        private readonly ILogger<AreasController> _logger;
        private readonly AreaService _areaService;
        public AreasController(IServiceProvider provider, ILogger<AreasController> logger, IHttpContextAccessor contextAccessor, AreaService areaService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _areaService = areaService;
        }

        [HttpGet("{areaId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetAreaDetailAsync(Guid areaId)
        {
            var subArea = await _areaService.GetAreaByIdAsync(areaId);

            return Ok(subArea);
        }

        [HttpGet("{districtId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetAreaDetailsAsync(Guid districtId, [FromQuery] Paging paging)
        {
            var subAreas = await _areaService.GetAreasAsync(districtId, paging);

            return Ok(subAreas);
        }

        [HttpPost("{districtId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddAreaAsync(Guid districtId, [FromForm] AddAreaRequest request)
        {
            await _areaService.AddAreaAsync(districtId, request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{areaId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeSubAreaAsync(Guid areaId, [FromForm] ChangeAreaRequest request)
        {
            await _areaService.ChangeAreaAsync(areaId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{areaId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteSubAreaAsync(Guid areaId)
        {
            await _areaService.CancelAreaAsync(areaId);

            return Ok(new { Deleted = "Success" });
        }

        [HttpGet("types"), ServiceLog]
        public async Task<OkObjectResult> GetAreaTypesAsync()
        {
            var types = await _areaService.GetAreaTypesAsync();

            return Ok(types);
        }
    }
}
