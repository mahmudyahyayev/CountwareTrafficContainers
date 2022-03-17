using CountwareTraffic.Services.Areas.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Mhd.Framework.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class DistrictService : IScopedSelfDependency
    {
        private readonly District.DistrictClient _districtClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;
        public DistrictService(District.DistrictClient districtClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _districtClient = districtClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetDistrictDetails> GetDistrictByIdAsync(Guid districtId)
        {
            GetDistrictRequest grpcRequest = new() { DistrictId = districtId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_districtClient.GetDistrictByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetDistrictDetails
            {
                Id = new Guid(grpcResponse.DistrictDetail.Id),
                CityId = new Guid(grpcResponse.DistrictDetail.CityId),
                Name = grpcResponse.DistrictDetail.Name,
                AuditCreateBy = new Guid(grpcResponse.DistrictDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.DistrictDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.DistrictDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.DistrictDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime
            };
        }

        public async Task<PagingResponse<GetDistrictDetails>> GetDistrictsAsync(Guid cityId, Paging paging)
        {
            GetDistrictsRequest grpcRequest = new()
            {
                CityId = cityId.ToString(),
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_districtClient.GetDistrictsAsync, grpcRequest, hasClientSideLog: false);

            var districts = grpcResponse.DistrictDetails.Select(district => new GetDistrictDetails
            {
                Id = new Guid(district.Id),
                Name = district.Name,
                CityId = new Guid(district.CityId),
                AuditCreateBy = new Guid(district.Audit.AuditCreateBy),
                AuditCreateDate = district.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(district.Audit.AuditModifiedBy),
                AuditModifiedDate = district.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
            });

            return new PagingResponse<GetDistrictDetails>(districts, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }

        public async Task AddDistrictAsync(Guid cityId, AddDistrictRequest request)
        {
            CreateDistrictRequest grpcRequest = new()
            {
                CityId = cityId.ToString(),
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_districtClient.AddDistrictAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task ChangeDistrictAsync(Guid districtId, ChangeDistrictRequest request)
        {
            UpdateDistrictRequest grpcRequest = new()
            {
                DistrictId = districtId.ToString(),
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_districtClient.ChangeDistrictAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task DeleteDistrictAsync(Guid districtId)
        {
            DeleteDistrictRequest grpcRequest = new() { DistrictId = districtId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_districtClient.DeleteDistrictAsync, grpcRequest, hasClientSideLog: false);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
