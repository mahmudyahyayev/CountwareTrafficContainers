using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Api.Controllers
{
    [Authorize]
    public class DevicesController : BaseController<DevicesController>
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public DevicesController(IServiceProvider provider, ILogger<DevicesController> logger, IHttpContextAccessor contextAccessor, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }


        [HttpGet("{deviceId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetDeviceDetailAsync(Guid deviceId)
        {
            var device = await _queryDispatcher.QueryAsync(new GetDevice
            {
                DeviceId = deviceId
            });

            return Ok(device);
        }

        [HttpGet("{subAreaId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetDeviceDetailsAsync(Guid subAreaId, [FromQuery] Paging request)
        {
            var devices = await _queryDispatcher.QueryAsync(new GetDevices
            {
                SubAreaId = subAreaId,
                PagingQuery = new PagingQuery(request.Page, request.Limit)
            });

            return Ok(devices);
        }

        [HttpPost("{subAreaId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddAsync(Guid subAreaId, [FromForm] CreateDeviceRequest request)
        {
            await _commandDispatcher.SendAsync(new CreateDevice
            {
                SubAreaId = subAreaId,
                Name = request.Name,
                Description = request.Description,
                Model = request.Model,
                DeviceTypeId = request.DeviceTypeId,
                IpAddress = request.IpAddress,
                Port = request.Port,
                Identity = request.Identity,
                Password = request.Password,
                UniqueId = request.UniqueId,
                MacAddress = request.MacAddress,

            });

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{deviceId}/change"), ServiceLog]
        public async Task<OkObjectResult> UpdateAsync(Guid deviceId, [FromForm] UpdateDeviceRequest request)
        {
            await _commandDispatcher.SendAsync(new UpdateDevice
            {
                DeviceId = deviceId,
                Description = request.Description,
                Name = request.Name,
                MacAddress = request.MacAddress,

            });

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{deviceId}/cancel"), ServiceLog]
        public async Task<OkObjectResult> DeleteAsync(Guid deviceId)
        {
            await _commandDispatcher.SendAsync(new DeleteDevice
            {
                DeviceId = deviceId
            });

            return Ok(new { Deleted = "Success" });
        }
    }
}
