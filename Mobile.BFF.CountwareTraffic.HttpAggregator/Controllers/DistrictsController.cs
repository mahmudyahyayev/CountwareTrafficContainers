using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Api;
using Sensormatic.Tool.Core;
using System;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers
{
    [Authorize]
    public class DistrictsController : BaseController<DistrictsController>
    {
        private readonly ILogger<DistrictsController> _logger;
        private readonly DistrictService _districtService;
        public DistrictsController(IServiceProvider provider, ILogger<DistrictsController> logger, IHttpContextAccessor contextAccessor, DistrictService districtService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _districtService = districtService;
        }

        [HttpGet("{districtId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetDistrictDetailAsync(Guid districtId)
        {
            var district = await _districtService.GetDistrictByIdAsync(districtId);

            return Ok(district);
        }

        [HttpGet("{cityId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetDistrictDetailsAsync(Guid cityId, [FromQuery] Paging paging)
        {
            var districts = await _districtService.GetDistrictsAsync(cityId, paging);

            return Ok(districts);
        }

        [HttpPost("{cityId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddDistrictAsync(Guid cityId, [FromForm] AddDistrictRequest request)
        {
            await _districtService.AddDistrictAsync(cityId, request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{districtId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeDistrictAsync(Guid districtId, [FromForm] ChangeDistrictRequest request)
        {
            await _districtService.ChangeDistrictAsync(districtId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{districtId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteDistrictAsync(Guid districtId)
        {
            await _districtService.DeleteDistrictAsync(districtId);

            return Ok(new { Deleted = "Success" });
        }
    }
}
