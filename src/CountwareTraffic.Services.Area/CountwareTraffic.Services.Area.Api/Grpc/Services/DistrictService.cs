using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [Authorize]
    public class DistrictService : District.DistrictBase
    {
        private readonly ILogger<DistrictService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public DistrictService(ILogger<DistrictService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public override async Task<GetDistrictDetailResponse> GetDistrictById(GetDistrictRequest request, ServerCallContext context)
        {
            var district = await _queryDispatcher.QueryAsync(new GetDistrict { DistrictId = request._DistrictId });

            GetDistrictDetailResponse response = new();

            response.DistrictDetail = new()
            {
                CityId = district.CityId.ToString(),
                Id = district.Id.ToString(),
                Name = district.Name,
                Audit = new Audit
                {
                    AuditCreateBy = district.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(district.AuditCreateDate),
                    AuditModifiedBy = district.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(district.AuditModifiedDate),
                }
            };

            return response;
        }

        public override async Task<DistrictPagingResponse> GetDistricts(GetDistrictsRequest request, ServerCallContext context)
        {
            var pagingDistricts = await _queryDispatcher.QueryAsync(new GetDistricts
            {
                CityId = request._CityId,
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            DistrictPagingResponse response = new()
            {
                TotalCount = pagingDistricts.TotalCount,
                HasNextPage = pagingDistricts.HasNextPage,
                Page = pagingDistricts.Page,
                Limit = pagingDistricts.Limit,
                Next = pagingDistricts.Next,
                Prev = pagingDistricts.Prev
            };

            pagingDistricts.Data.ToList().ForEach(district => response.DistrictDetails.Add(new DistrictDetail
            {
                Id = district.Id.ToString(),
                Name = district.Name,
                CityId = district.CityId.ToString(),
                Audit = new Audit
                {
                    AuditCreateBy = district.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(district.AuditCreateDate),
                    AuditModifiedBy = district.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(district.AuditModifiedDate),
                }
            }));

            return response;
        }

        public override async Task<CreateSuccessResponse> AddDistrict(CreateDistrictRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateDistrict
            {
                CityId = request._CityId,
                Name = request.Name,
            });

            return new CreateSuccessResponse { Created = "Created" };
        }

        public override async Task<UpdateSuccessResponse> ChangeDistrict(UpdateDistrictRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateDistrict
            {
                Name = request.Name,
                DistrictId = request._DistrictId
            });

            return new UpdateSuccessResponse { Updated = "Updated" };
        }

        public override async Task<DeleteSuccessResponse> DeleteDistrict(DeleteDistrictRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteDistrict { DistrictId = request._DistrictId });
            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }
    }
}
