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
    public class CompaniesController : BaseController<CompaniesController>
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly CompanyService _companyService;
        public CompaniesController(IServiceProvider provider, ILogger<CompaniesController> logger, IHttpContextAccessor contextAccessor, CompanyService companyService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet("{companyId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetCompanyDetailAsync(Guid companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);

            return Ok(company);
        }

        [HttpGet("details"), ServiceLog]
        public async Task<OkObjectResult> GetCompanyDetailsAsync([FromQuery] Paging paging)
        {
            var companies = await _companyService.GetCompaniesAsync(paging);

            return Ok(companies);
        }

        [HttpPost("add"), ServiceLog]
        public async Task<OkObjectResult> AddCompanyAsync([FromBody] AddCompanyRequest request)
        {
            await _companyService.AddCompanyAsync(request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{companyId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeCompanyAsync(Guid companyId, [FromForm] ChangeCompanyRequest request)
        {
            await _companyService.ChangeCompanyAsync(companyId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{companyId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteCompanyAsync(Guid companyId)
        {
            await _companyService.DeleteCompanyAsync(companyId);

            return Ok(new { Deleted = "Success" });
        }
    }
}
