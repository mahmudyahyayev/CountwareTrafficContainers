using CountwareTraffic.Services.Areas.Grpc;
using Mobile.BFF.CountwareTraffic.HttpAggregator.Grpc;
using Mhd.Framework.Grpc.Client;
using Mhd.Framework.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class SubAreaService : IScopedSelfDependency
    {
        private readonly Subarea.SubareaClient _subAreaClient;
        private readonly AsyncUnaryCallHandler _asyncUnaryCallHandler;
       
        public SubAreaService(Subarea.SubareaClient subAreaClient, AsyncUnaryCallHandler asyncUnaryCallHandler)
        {
            _subAreaClient = subAreaClient;
            _asyncUnaryCallHandler = asyncUnaryCallHandler;
        }

        public async Task<GetSubAreaDetails> GetSubAreaByIdAsync(Guid subAreaId)
        {
            GetSubAreaRequest grpcRequest = new() { SubAreaId = subAreaId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_subAreaClient.GetSubAreaByIdAsync, grpcRequest, hasClientSideLog: false);

            return new GetSubAreaDetails
            {
                Id = new Guid(grpcResponse.SubAreaDetail.Id),
                Name = grpcResponse.SubAreaDetail.Name,
                Description = grpcResponse.SubAreaDetail.Description,
                AreaId = new Guid(grpcResponse.SubAreaDetail.AreaId),
                AuditCreateBy = new Guid(grpcResponse.SubAreaDetail.Audit.AuditCreateBy),
                AuditCreateDate = grpcResponse.SubAreaDetail.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(grpcResponse.SubAreaDetail.Audit.AuditModifiedBy),
                AuditModifiedDate = grpcResponse.SubAreaDetail.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
            };
        }

        public async Task<PagingResponse<GetSubAreaDetails>> GetSubAreasAsync(Guid areaId, Paging paging)
        {
            GetSubAreasRequest grpcRequest = new()
            {
                AreaId = areaId.ToString(),
                PagingRequest = new PagingRequest
                {
                    Limit = paging.Limit,
                    Page = paging.Page
                }
            };

            var grpcResponse = await _asyncUnaryCallHandler
                .CallMethodAsync(_subAreaClient.GetSubAreasAsync, grpcRequest, hasClientSideLog: false);

            var subAreas = grpcResponse.SubAreaDetails.Select(u => new GetSubAreaDetails
            {
                Id = new Guid(u.Id),
                Name = u.Name,
                Description = u.Description,
                AreaId = new Guid(u.AreaId),
                AuditCreateBy = new Guid(u.Audit.AuditCreateBy),
                AuditCreateDate = u.Audit.AuditCreateDate.ToDateTimeOffset().LocalDateTime,
                AuditModifiedBy = new Guid(u.Audit.AuditModifiedBy),
                AuditModifiedDate = u.Audit.AuditModifiedDate.ToDateTimeOffset().LocalDateTime,
            });

            return new PagingResponse<GetSubAreaDetails>(subAreas, grpcResponse.TotalCount, grpcResponse.Page, grpcResponse.Limit, grpcResponse.HasNextPage);
        }

        public async Task AddSubAreaAsync(Guid areaId, AddSubAreaRequest request)
        {
            CreateSubAreaRequest grpcRequest = new()
            {
                AreaId = areaId.ToString(),
                Description = request.Description,
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_subAreaClient.AddSubAreaAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task ChangeSubAreaAsync(Guid subAreaId, ChangeSubAreaRequest request)
        {
            UpdateSubAreaRequest grpcRequest = new()
            {
                SubAreaId = subAreaId.ToString(),
                Description = request.Description,
                Name = request.Name
            };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_subAreaClient.ChangeSubAreaAsync, grpcRequest, hasClientSideLog: false);
        }

        public async Task DeleteSubAreaAsync(Guid subAreaId)
        {
            DeleteSubAreaRequest grpcRequest = new() { SubAreaId = subAreaId.ToString() };

            var grpcResponse = await _asyncUnaryCallHandler
               .CallMethodAsync(_subAreaClient.DeleteSubAreaAsync, grpcRequest, hasClientSideLog: false);
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
