using CountwareTraffic.Services.Devices.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Mhd.Framework.Grpc.Client;
using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class DeviceService : IScopedSelfDependency
    {
        private readonly Device.DeviceClient _deviceClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;
        public DeviceService(Device.DeviceClient deviceClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _deviceClient = deviceClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetDeviceDetails> GetDeviceByIdAsync(Guid deviceId)
        {
            GetDeviceRequest grpcRequest = new() { DeviceId = deviceId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_deviceClient.GetDeviceByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetDeviceDetails
            {
                Id = new Guid(grpcResponse.DeviceDetail.Id),
                Name = grpcResponse.DeviceDetail.Name,
                Description = grpcResponse.DeviceDetail.Description,
                AuditCreateBy = new Guid(grpcResponse.DeviceDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.DeviceDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.DeviceDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.DeviceDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                DeviceStatusId = grpcResponse.DeviceDetail.DeviceStatusId,
                DeviceStatusName = grpcResponse.DeviceDetail.DeviceStatusName,
                DeviceTypeId = grpcResponse.DeviceDetail.DeviceTypeId,
                DeviceTypeName = grpcResponse.DeviceDetail.DeviceTypeName,
                Identity = grpcResponse.DeviceDetail.Identity,
                IpAddress = grpcResponse.DeviceDetail.IpAddress,
                MacAddress = grpcResponse.DeviceDetail.MacAddress,
                Model = grpcResponse.DeviceDetail.Model,
                Password = grpcResponse.DeviceDetail.Password,
                Port = grpcResponse.DeviceDetail.Port,
                UniqueId = grpcResponse.DeviceDetail.UniqueId,
                SubAreaId = new Guid(grpcResponse.DeviceDetail.SubAreaId)
            };
        }

        public async Task<PagingResponse<GetDeviceDetails>> GetDevicesAsync(Guid subAreaId, Paging paging)
        {
            GetDevicesRequest grpcRequest = new()
            {
                SubAreaId = subAreaId.ToString(),
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_deviceClient.GetDevicesAsync, grpcRequest, hasClientSideLog: false);

            var devices = grpcResponse.DeviceDetails.Select(device => new GetDeviceDetails
            {
                Id = new Guid(device.Id),
                Name = device.Name,
                Description = device.Description,
                AuditCreateBy = new Guid(device.Audit.AuditCreateBy),
                AuditCreateDate = device.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(device.Audit.AuditModifiedBy),
                AuditModifiedDate = device.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                DeviceStatusId = device.DeviceStatusId,
                DeviceStatusName = device.DeviceStatusName,
                DeviceTypeId = device.DeviceTypeId,
                DeviceTypeName = device.DeviceTypeName,
                Identity = device.Identity,
                IpAddress = device.IpAddress,
                MacAddress = device.MacAddress,
                Model = device.Model,
                Password = device.Password,
                Port = device.Port,
                UniqueId = device.UniqueId,
                SubAreaId = new Guid(device.SubAreaId)
            });

            return new PagingResponse<GetDeviceDetails>(devices, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }


        public async Task AddDeviceAsync(Guid subAreaId, AddDeviceRequest request)
        {
            CreateDeviceRequest grpcRequest = new()
            {
                SubAreaId = subAreaId.ToString(),
                Name = request.Name,
                UniqueId = request.UniqueValue,
                Description = request.Description,
                DeviceTypeId = request.DeviceTypeId,
                Identity = request.Identity,
                IpAddress = request.IpAddress,
                MacAddress = request.MacAddress,
                Model = request.Model,
                Password = request.Password,
                Port = request.Port,
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_deviceClient.AddDeviceAsync, grpcRequest, hasClientSideLog: false);
        }


        public async Task ChangeDeviceAsync(Guid deviceId, ChangeDeviceRequest request)
        {
            UpdateDeviceRequest grpcRequest = new()
            {
                DeviceId = deviceId.ToString(),
                DeviceTypeId = request.DeviceTypeId,
                Port = request.Port,
                Password = request.Password,
                Model = request.Model,
                MacAddress = request.MacAddress,
                DeviceStatusId = request.DeviceStatusId,
                Description = request.Description,
                Identity = request.Identity,
                IpAddress = request.IpAddress,
                Name = request.Name,
                UniqueId = request.UniqueId
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_deviceClient.ChangeDeviceAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task DeleteDeviceAsync(Guid deviceId)
        {
            DeleteDeviceRequest grpcRequest = new() { DeviceId = deviceId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_deviceClient.DeleteDeviceAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task<IEnumerable<GetDeviceType>> GetDeviceTypesAsync()
        {
            GetDeviceTypesRequest grpcRequest = new();

            var grpcResponse = await _asyncUnaryCallHandler
              .CallMethodAsync(_deviceClient.GetDeviceTypesAsync, grpcRequest, hasClientSideLog: false);

            return grpcResponse.DeviceTypes.Select(type => new GetDeviceType
            {
                Id = type.Id,
                Name = type.Name
            });
        }

        public async Task<IEnumerable<GetDeviceStatus>> GetDeviceStatusesAsync()
        {
            GetDeviceStatusesRequest grpcRequest = new();

            var grpcResponse = await _asyncUnaryCallHandler
              .CallMethodAsync(_deviceClient.GetDeviceStatusesAsync, grpcRequest, hasClientSideLog: false);

            return grpcResponse.DeviceStatuses.Select(type => new GetDeviceStatus
            {
                Id = type.Id,
                Name = type.Name
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
