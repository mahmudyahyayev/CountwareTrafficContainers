using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sensormatic.Tool.Core;
using System;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers
{
    [Authorize]
    public class DeviceController : BaseController<DeviceController>
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly DeviceService _deviceService;
        public DeviceController(IServiceProvider provider, ILogger<DeviceController> logger, IHttpContextAccessor contextAccessor, DeviceService deviceService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _deviceService = deviceService;
        }

        [HttpGet("{deviceId}/detail"), ServiceLog]
        public async Task<OkObjectResult> GetDeviceDetailAsync(Guid deviceId)
        {
            var device = await _deviceService.GetDeviceByIdAsync(deviceId);

            return Ok(device);
        }

        [HttpGet("{subAreaId}/details"), ServiceLog]
        public async Task<OkObjectResult> GetDeviceDetailsAsync(Guid subAreaId, [FromQuery] Paging paging)
        {
            var devices = await _deviceService.GetDevicesAsync(subAreaId, paging);

            return Ok(devices);
        }

        [HttpPost("{subAreaId}/add"), ServiceLog]
        public async Task<OkObjectResult> AddDeviceAsync(Guid subAreaId, [FromForm] AddDeviceRequest request)
        {
            await _deviceService.AddDeviceAsync(subAreaId, request);

            return Ok(new { Created = "Success" });
        }

        [HttpPut("{deviceId}/change"), ServiceLog]
        public async Task<OkObjectResult> ChangeDeviceAsync(Guid deviceId, [FromForm] ChangeDeviceRequest request)
        {
            await _deviceService.ChangeDeviceAsync(deviceId, request);

            return Ok(new { Updated = "Success" });
        }

        [HttpDelete("{deviceId}/delete"), ServiceLog]
        public async Task<OkObjectResult> DeleteCountryAsync(Guid deviceId)
        {
            await _deviceService.DeleteDeviceAsync(deviceId);

            return Ok(new { Deleted = "Success" });
        }

        [HttpGet("statuses"), ServiceLog]
        public async Task<OkObjectResult> GetDeviceStatusesAsync()
        {
            var statuses = await _deviceService.GetDeviceStatusesAsync();

            return Ok(statuses);
        }

        [HttpGet("types"), ServiceLog]
        public async Task<OkObjectResult> GetDeviceTypesAsync()
        {
            var types = await _deviceService.GetDeviceTypesAsync();

            return Ok(types);
        }
    }
}
