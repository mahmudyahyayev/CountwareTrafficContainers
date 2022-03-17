using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mhd.Framework.Api;
using Mhd.Framework.Common;
using Mhd.Framework.Core;
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
        private readonly DeviceService _deviceService;
        public SubAreasController(IServiceProvider provider, ILogger<SubAreasController> logger, IHttpContextAccessor contextAccessor, SubAreaService subAreaService, DeviceService deviceService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _subAreaService = subAreaService;
            _deviceService = deviceService;
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
        public async Task<ApiResponse<AddResponse>> AddSubAreaAsync(Guid areaId, [FromForm] AddSubAreaRequest request)
        {
            await _subAreaService.AddSubAreaAsync(areaId, request);

            return Response(new AddResponse { Created = "Success"}, new ResultMessage()
            {
                CallToActionType = CallToActionType.Ok,
                Description = $"Subarea has been created.",
                Title = "Subarea has been created."
            });
        }

        [HttpPut("{subAreaId}/change"), ServiceLog]
        public async Task<ApiResponse<ChangeResponse>> ChangeSubAreaAsync(Guid subAreaId, [FromForm] ChangeSubAreaRequest request)
        {
            await _subAreaService.ChangeSubAreaAsync(subAreaId, request);

            return Response(new ChangeResponse { Updated = "Success", Id = subAreaId }, new ResultMessage()
            {
                CallToActionType = CallToActionType.Ok,
                Description = $"Subarea with {subAreaId} id has been updated.",
                Title = "Subarea with {subAreaId} id has been updated."
            });
        }


        [HttpDelete("{subAreaId}/delete"), ServiceLog]
        public async Task<ApiResponse<DeleteResponse>> DeleteSubAreaAsync(Guid subAreaId)
        {
            var subAreaDeviceControl = await _deviceService.GetDevicesAsync(subAreaId, new Paging());

            if (subAreaDeviceControl.TotalCount > 0)
            {
                return Response(new DeleteResponse { Deleted = "Failed", Id = subAreaId }, new ResultMessage()
                {
                    CallToActionType = CallToActionType.GoToDevices,
                    Description = $"A device has been defined in the SubaArea with id {subAreaId}.",
                    Title = "SubArea with id {subAreaId} cannot be deleted."
                });
            }

            await _subAreaService.DeleteSubAreaAsync(subAreaId);

            return Response(new DeleteResponse { Deleted = "Success", Id = subAreaId }, new ResultMessage()
            {
                CallToActionType = CallToActionType.Ok,
                Description = $"Subarea with {subAreaId} id has been deleted.",
                Title = "Subarea with {subAreaId} id has been deleted."
            });
        }
    }
}
