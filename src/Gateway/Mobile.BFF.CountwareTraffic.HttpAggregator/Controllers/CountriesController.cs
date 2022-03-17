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
    public class CountriesController : BaseController<CountriesController>
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly CountryService _countryService;
        public CountriesController(IServiceProvider provider, ILogger<CountriesController> logger, IHttpContextAccessor contextAccessor, CountryService countryService) : base(provider, logger, contextAccessor.HttpContext)

        {
            _logger = logger;
            _countryService = countryService;
        }

        [HttpGet("{countryId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetCountryDetailAsync(Guid countryId)
        {
            var city = await _countryService.GetCountryByIdAsync(countryId);

            return Ok(city);
        }

        [HttpGet("{companyId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetCountryDetailsAsync(Guid companyId, [FromQuery] Paging paging)
        {
            var cities = await _countryService.GetCountriesAsync(companyId, paging);

            return Ok(cities);
        }

        [HttpPost("{companyId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddCountryAsync(Guid companyId, [FromForm] AddCountryRequest request)
        {
            await _countryService.AddCountryAsync(companyId, request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{countryId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeCountryAsync(Guid countryId, [FromForm] ChangeCountryRequest request)
        {
            await _countryService.ChangeCountryAsync(countryId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{countryId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteCountryAsync(Guid countryId)
        {
            await _countryService.DeleteCountryAsync(countryId);

            return Ok(new { Deleted = "Success" });
        }
    }
}
