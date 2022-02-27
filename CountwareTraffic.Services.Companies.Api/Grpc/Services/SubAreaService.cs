using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Grpc
{
    [Authorize]
    public class SubAreaService : Subarea.SubareaBase
    {
        private readonly ILogger<SubAreaService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public SubAreaService(ILogger<SubAreaService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        public override async Task<GetSubAreaDetailResponse> GetSubAreaById(GetSubAreaRequest request, ServerCallContext context)
        {
            var subArea = await _queryDispatcher.QueryAsync(new GetSubArea { SubAreaId = request._SubAreaId });

            var response = new GetSubAreaDetailResponse();

            response.SubAreaDetail = new SubAreaDetail
            {
                Id = subArea.Id.ToString(),
                Name = subArea.Name,
                Description = subArea.Description,
                AreaId = subArea.AreaId.ToString(),
                Audit = new Audit
                {
                    AuditCreateBy = subArea.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(subArea.AuditCreateDate),
                    AuditModifiedBy = subArea.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(subArea.AuditModifiedDate),
                }
            };
            return response;
        }

        public override async Task<SubAreaPagingResponse> GetSubAreas(GetSubAreasRequest request, ServerCallContext context)
        {
            var pagingSubAreas = await _queryDispatcher.QueryAsync(new GetSubAreas
            {
                AreaId = request._AreaId,
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            SubAreaPagingResponse response = new()
            {
                TotalCount = pagingSubAreas.TotalCount,
                HasNextPage = pagingSubAreas.HasNextPage,
                Page = pagingSubAreas.Page,
                Limit = pagingSubAreas.Limit,
                Next = pagingSubAreas.Next,
                Prev = pagingSubAreas.Prev
            };

            pagingSubAreas.Data.ToList().ForEach(subArea => response.SubAreaDetails.Add(new SubAreaDetail
            {
                Id = subArea.Id.ToString(),
                Name = subArea.Name,
                Description = subArea.Description,
                AreaId = subArea.AreaId.ToString(),
                Audit = new Audit
                {
                    AuditCreateBy = subArea.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(subArea.AuditCreateDate),
                    AuditModifiedBy = subArea.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(subArea.AuditModifiedDate)
                }
            }));

            return response;
        }


        public override async Task<CreateSuccessResponse> AddSubArea(CreateSubAreaRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateSubArea
            {
                AreaId = request._AreaId,
                Name = request.Name,
                Description = request.Description
            });

            return new CreateSuccessResponse { Created = "Created" };
        }

        public override async Task<UpdateSuccessResponse> ChangeSubArea(UpdateSubAreaRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateSubArea
            {
                SubAreaId = request._SubAreaId,
                Name = request.Name,
                Description = request.Description
            });

            return new UpdateSuccessResponse { Updated = "Updated" };;
        }

        public override async Task<DeleteSuccessResponse> DeleteSubArea(DeleteSubAreaRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteSubArea
            {
                SubAreaId = request._SubAreaId
            });

            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }
    }
}
