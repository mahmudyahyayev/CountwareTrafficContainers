using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mobile.BFF.CountwareTraffic.HttpAggregator;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers;
using Sensormatic.Tool.Api;
using Sensormatic.Tool.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Api.Controllers
{
    [Authorize]
    public class CitiesController : BaseController<CitiesController>
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly CityService _cityService;
        public CitiesController(IServiceProvider provider, ILogger<CitiesController> logger, IHttpContextAccessor contextAccessor, CityService cityService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [HttpGet("{cityId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetCityDetailAsync(Guid cityId)
        {
            var city = await _cityService.GetCityByIdAsync(cityId);

            return Ok(city);
        }

        [HttpGet("{countryId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetCityDetailsAsync(Guid countryId, [FromQuery] Paging paging)
        {
            var cities = await _cityService.GetCitiesAsync(countryId, paging);

            return Ok(cities);
        }

        [HttpPost("{countryId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddCityAsync(Guid countryId, [FromForm] AddCityRequest request)
        {
            await _cityService.AddCityAsync(countryId, request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{cityId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeCityAsync(Guid cityId, [FromForm] ChangeCityRequest request)
        {
            await _cityService.ChangeCityAsync(cityId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{cityId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteCityAsync(Guid cityId)
        {
            await _cityService.DeleteCityAsync(cityId);

            return Ok(new { Deleted = "Success" });
        }
    }
}
