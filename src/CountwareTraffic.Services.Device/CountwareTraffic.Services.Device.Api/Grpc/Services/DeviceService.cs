using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Devices.Application;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Grpc
{
    [Authorize]
    public class DeviceService : Device.DeviceBase
    {
        private readonly ILogger<DeviceService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public DeviceService(ILogger<DeviceService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public async override Task<GetDeviceDetailResponse> GetDeviceById(GetDeviceRequest request, ServerCallContext context)
        {
            var device = await _queryDispatcher.QueryAsync(new GetDevice { DeviceId = request._DeviceId });

            var response = new GetDeviceDetailResponse();

            response.DeviceDetail = new DeviceDetail
            {
                Id = device.Id.ToString(),
                Name = device.Name,
                Description = device.Description,
                DeviceStatusId = device.DeviceStatusId,
                DeviceTypeId = device.DeviceTypeId,
                Identity = device.Identity,
                IpAddress = device.IpAddress,
                MacAddress = device.MacAddress,
                Model = device.Model,
                Password = device.Password,
                Port = device.Port,
                SubAreaId = device.SubAreaId.ToString(),
                UniqueId = device.UniqueId,
                DeviceStatusName = device.DeviceStatusName,
                DeviceTypeName = device.DeviceTypeName,
                Audit = new Audit
                {
                    AuditCreateBy = device.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(device.AuditCreateDate),
                    AuditModifiedBy = device.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(device.AuditModifiedDate),
                }
            };
            return response;
        }

        public async override Task<DevicePagingResponse> GetDevices(GetDevicesRequest request, ServerCallContext context)
        {
            var pagingDevices = await _queryDispatcher.QueryAsync(new GetDevices
            {
                SubAreaId = request._SubAreaId,
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            DevicePagingResponse response = new()
            {
                TotalCount = pagingDevices.TotalCount,
                HasNextPage = pagingDevices.HasNextPage,
                Page = pagingDevices.Page,
                Limit = pagingDevices.Limit,
                Next = pagingDevices.Next,
                Prev = pagingDevices.Prev
            };

            pagingDevices.Data.ToList().ForEach(device => response.DeviceDetails.Add(new DeviceDetail
            {
                Id = device.Id.ToString(),
                Name = device.Name,
                Description = device.Description,
                DeviceStatusId = device.DeviceStatusId,
                DeviceTypeId = device.DeviceTypeId,
                Identity = device.Identity,
                IpAddress = device.IpAddress,
                MacAddress = device.MacAddress,
                Model = device.Model,
                Password = device.Password,
                Port = device.Port,
                SubAreaId = device.SubAreaId.ToString(),
                UniqueId = device.UniqueId,
                DeviceStatusName = device.DeviceStatusName,
                DeviceTypeName = device.DeviceTypeName,
                Audit = new Audit
                {
                    AuditCreateBy = device.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(device.AuditCreateDate),
                    AuditModifiedBy = device.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(device.AuditModifiedDate),
                }
            }));
            return response;
        }

        public async override Task<CreateSuccessResponse> AddDevice(CreateDeviceRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateDevice
            {
                SubAreaId = request._SubAreaId,
                Description = request.Description,
                DeviceTypeId = request.DeviceTypeId,
                Identity = request.Identity,
                IpAddress = request.IpAddress,
                MacAddress = request.MacAddress,
                Model = request.Model,
                Name = request.Name,
                Password = request.Password,
                Port = request.Port,
                UniqueId = request.UniqueId
            });

            return new CreateSuccessResponse { Created = "Created" };

        }

        public async override Task<UpdateSuccessResponse> ChangeDevice(UpdateDeviceRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateDevice
            {
                DeviceId = request._DeviceId,
                Description = request.Description,
                DeviceStatusId = request.DeviceStatusId,
                DeviceTypeId = request.DeviceTypeId,
                Identity = request.Identity,
                IpAddress = request.IpAddress,
                MacAddress = request.MacAddress,
                Model = request.Model,
                Name = request.Name,
                Password = request.Password,
                Port = request.Port,
                UniqueId = request.UniqueId,
            });
            return new UpdateSuccessResponse { Updated = "Updated" };

        }

        public async override Task<DeleteSuccessResponse> DeleteDevice(DeleteDeviceRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteDevice { DeviceId = request._DeviceId });
            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }

        public async override Task<GetDeviceStatusesResponse> GetDeviceStatuses(GetDeviceStatusesRequest request, ServerCallContext context)
        {
            var deviceStatuses = await _queryDispatcher.QueryAsync(new GetDeviceStatuses { });

            GetDeviceStatusesResponse response = new();

            deviceStatuses.ToList().ForEach(type => response.DeviceStatuses.Add(new DeviceStatus
            {
                Id = type.Id,
                Name = type.Name
            }));

            return response;
        }

        public async override Task<GetDeviceTypesResponse> GetDeviceTypes(GetDeviceTypesRequest request, ServerCallContext context)
        {
            var deviceTypes = await _queryDispatcher.QueryAsync(new GetDeviceTypes { });

            GetDeviceTypesResponse response = new();

            deviceTypes.ToList().ForEach(type => response.DeviceTypes.Add(new DeviceType
            {
                Id = type.Id,
                Name = type.Name
            }));

            return response;
        }
    }
}
