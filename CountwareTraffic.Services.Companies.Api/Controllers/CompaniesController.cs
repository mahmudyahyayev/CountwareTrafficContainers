using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Api;
using Sensormatic.Tool.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Api.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController<CompaniesController>
    {

        private readonly ILogger<CompaniesController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public CompaniesController(IServiceProvider provider, ILogger<CompaniesController> logger, IHttpContextAccessor contextAccessor, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }


        [HttpGet("{companyId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetCompanyDetailAsync(Guid companyId)
        {
            var company = await _queryDispatcher.QueryAsync(new GetCompany
            {
                CompanyId = companyId
            });

            return Ok(company);
        }

        [HttpGet("details"), ServiceLog]
        public async Task<OkObjectResult> GetCompanyDetailsAsync([FromQuery] Paging request)
        {
            var companies = await _queryDispatcher.QueryAsync(new GetCompanies
            {
                PagingQuery = new PagingQuery(request.Page, request.Limit)
            });

            return Ok(companies);
        }


        [HttpPost("add"), ServiceLog]
        public async Task<OkObjectResult> AddCompanyAsync([FromBody] CreateCompanyRequest request) //8cce9756-bb70-4d64-b1ee-a7dd0383d6e7
        {
            await _commandDispatcher.SendAsync(new CreateCompany
            {
                City = request.City,
                Country = request.Country,
                Description = request.Description,
                EmailAddress = request.EmailAddress,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                State = request.State,
                Street = request.Street,
                ZipCode = request.ZipCode,
            });

            return Ok(new { Created = "Success" });
        }


        [HttpPut("change"), ServiceLog]
        public async Task<OkObjectResult> UpdateAsync([FromBody] UpdateCompanyRequest request)
        {
            //todo:
            return Ok(new { Updated = "Success" });
        }


        [HttpDelete("{companyId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteAsync(Guid companyId)
        {
            //todo:

            return Ok(new { Deleted = "Success" });
        }
    }
}
