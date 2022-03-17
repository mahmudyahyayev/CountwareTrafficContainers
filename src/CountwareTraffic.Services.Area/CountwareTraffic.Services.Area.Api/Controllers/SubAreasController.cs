using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Api;
using Mhd.Framework.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Api.Controllers
{
    [Authorize]
    public class SubAreasController : BaseController<SubAreasController>
    {
        private readonly ILogger<SubAreasController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SubAreasController(IServiceProvider provider, ILogger<SubAreasController> logger, IHttpContextAccessor contextAccessor, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }


        [HttpGet("{subAreaId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetSubAreaDetailAsync(Guid subAreaId)
        {
            var subArea = await _queryDispatcher.QueryAsync(new GetSubArea
            {
                SubAreaId = subAreaId
            });

            return Ok(subArea);
        }


        [HttpGet("{areaId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetSubAreaDetailsAsync(Guid areaId, [FromQuery] Paging request)
        {
            var subAreas = await _queryDispatcher.QueryAsync(new GetSubAreas
            {
                AreaId = areaId,
                PagingQuery = new PagingQuery(request.Page, request.Limit)
            });

            return Ok(subAreas);
        }


        [HttpPost("{areaId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddAsync(Guid areaId, [FromForm] CreateSubAreaRequest request) //8cce9756-bb70-4d64-b1ee-a7dd0383d6e7
        {
            await _commandDispatcher.SendAsync(new CreateSubArea
            {
                AreaId = areaId,
                Description = request.Description,
                Name = request.Name
            });

            return Ok(new { Created = "Success" });
        }


        [HttpPut("{subAreaId}/change"), ServiceLog]
        public async Task<OkObjectResult> UpdateAsync(Guid subAreaId, [FromForm] UpdateSubAreaRequest request)
        {
            await _commandDispatcher.SendAsync(new UpdateSubArea
            {
                SubAreaId = subAreaId,
                Description = request.Description,
                Name = request.Name
            });

            return Ok(new { Updated = "Success" });
        }


        [HttpDelete("{subAreaId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteAsync(Guid subAreaId)
        {
            await _commandDispatcher.SendAsync(new DeleteSubArea
            {
                 SubAreaId = subAreaId
            });

            return Ok(new { Deleted = "Success" });
        }
    }
}
