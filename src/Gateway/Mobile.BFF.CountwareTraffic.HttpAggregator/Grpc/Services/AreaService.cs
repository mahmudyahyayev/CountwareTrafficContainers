using CountwareTraffic.Services.Areas.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Mhd.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class AreaService : IScopedSelfDependency
    {
        private readonly Area.AreaClient _areaClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;

        public AreaService(Area.AreaClient areaClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _areaClient = areaClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetAreaDetails> GetAreaByIdAsync(Guid areaId)
        {
            GetAreaRequest grpcRequest = new() { AreaId = areaId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_areaClient.GetAreaByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetAreaDetails
            {
                Id = new Guid(grpcResponse.AreaDetail.Id),
                Name = grpcResponse.AreaDetail.Name,
                Description = grpcResponse.AreaDetail.Description,
                DistrictId = new Guid(grpcResponse.AreaDetail.DistrictId),
                AuditCreateBy = new Guid(grpcResponse.AreaDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.AreaDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.AreaDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.AreaDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                AreaTypeName = grpcResponse.AreaDetail.AreaTypeName,
                EmailAddress = grpcResponse.AreaDetail.EmailAddress,
                FaxNumber = grpcResponse.AreaDetail.FaxNumber,
                GsmNumber = grpcResponse.AreaDetail.GsmNumber,
                Latitude = grpcResponse.AreaDetail.Latitude,
                Longitude = grpcResponse.AreaDetail.Longitude,
                PhoneNumber = grpcResponse.AreaDetail.PhoneNumber,
                Street = grpcResponse.AreaDetail.Street,
                AreaTypeId = grpcResponse.AreaDetail.AreaTypeId
            };
        }

        public async Task<PagingResponse<GetAreaDetails>> GetAreasAsync(Guid districtId, Paging paging)
        {
            GetAreasRequest grpcRequest = new()
            {
                DistrictId = districtId.ToString(),
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_areaClient.GetAreasAsync, grpcRequest, hasClientSideLog: false);

            var areas = grpcResponse.AreaDetails.Select(area => new GetAreaDetails
            {
                Id = new Guid(area.Id),
                Name = area.Name,
                Description = area.Description,
                DistrictId = new Guid(area.DistrictId),
                AuditCreateBy = new Guid(area.Audit.AuditCreateBy),
                AuditCreateDate = area.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(area.Audit.AuditModifiedBy),
                AuditModifiedDate = area.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
                AreaTypeName = area.AreaTypeName,
                EmailAddress = area.EmailAddress,
                FaxNumber = area.FaxNumber,
                GsmNumber = area.GsmNumber,
                Latitude = area.Latitude,
                Longitude = area.Longitude,
                PhoneNumber = area.PhoneNumber,
                Street = area.Street,
                AreaTypeId = area.AreaTypeId
            });

            return new PagingResponse<GetAreaDetails>(areas, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }

        public async Task AddAreaAsync(Guid districtId, AddAreaRequest request)
        {
            CreateAreaRequest grpcRequest = new()
            {
                DistrictId = districtId.ToString(),
                Description = request.Description,
                Name = request.Name,
                AreaTypeId = request.AreaTypeId,
                Street = request.Street,
                PhoneNumber2 = request.PhoneNumber2,
                EmailAddress = request.EmailAddress,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PhoneNumber = request.PhoneNumber,
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_areaClient.AddAreaAsync, grpcRequest, hasClientSideLog: true);
        }

        public async Task ChangeAreaAsync(Guid areaId, ChangeAreaRequest request)
        {
            UpdateAreaRequest grpcRequest = new()
            {
                AreaId = areaId.ToString(),
                Description = request.Description,
                Name = request.Name,
                AreaTypId = request.AreaTypeId,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                GsmNumber = request.GsmNumber,
                FaxNumber = request.FaxNumber,
                PhoneNumber2 = request.PhoneNumber2,
                Street = request.Street
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_areaClient.ChangeAreaAsync, grpcRequest, hasClientSideLog: true);
        }

        public async Task CancelAreaAsync(Guid areaId)
        {
            DeleteAreaRequest grpcRequest = new() { AreaId = areaId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_areaClient.DeleteAreaAsync, grpcRequest, hasClientSideLog: true);
        }

        public async Task<IEnumerable<GetAreaType>> GetAreaTypesAsync()
        {
            GetAreaTypesRequest grpcRequest = new();

            var grpcResponse = await _asyncUnaryCallHandler
              .CallMethodAsync(_areaClient.GetAreaTypesAsync, grpcRequest, hasClientSideLog: false);

            return grpcResponse.AreaTypes.Select(type => new GetAreaType
            {
                Id = type.Id,
                Name = type.Name
            });
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
