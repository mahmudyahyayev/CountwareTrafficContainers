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
    public class AreaService : Area.AreaBase
    {
        private readonly ILogger<AreaService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public AreaService(ILogger<AreaService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public override async Task<GetAreaDetailResponse> GetAreaById(GetAreaRequest request, ServerCallContext context)
        {
            //context.GetUser();
            var area = await _queryDispatcher.QueryAsync(new GetArea { AreaId = request._AreaId });

            GetAreaDetailResponse response = new();

            response.AreaDetail = new()
            {
                Id = area.Id.ToString(),
                Name = area.Name,
                Description = area.Description,
                DistrictId = area.DistrictId.ToString(),
                AreaTypeName = area.AreaTypeName,
                EmailAddress = area.EmailAddress,
                FaxNumber = area.FaxNumber,
                GsmNumber = area.GsmNumber,
                Latitude = area.Latitude,
                Longitude = area.Longitude,
                PhoneNumber = area.PhoneNumber,
                Street = area.Street,
                AreaTypeId = area.AreaTypeId,
                PhoneNumber2 = area.PhoneNumber2,
                Audit = new Audit
                {
                    AuditCreateBy = area.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(area.AuditCreateDate),
                    AuditModifiedBy = area.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(area.AuditModifiedDate),
                }
            };
            return response;
        }


        public override async Task<AreaPagingResponse> GetAreas(GetAreasRequest request, ServerCallContext context)
        {
            var pagingAreas = await _queryDispatcher.QueryAsync(new GetAreas
            {
                DistrictId = request._DistrictId,
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            AreaPagingResponse response = new()
            {
                TotalCount = pagingAreas.TotalCount,
                HasNextPage = pagingAreas.HasNextPage,
                Page = pagingAreas.Page,
                Limit = pagingAreas.Limit,
                Next = pagingAreas.Next,
                Prev = pagingAreas.Prev
            };

            pagingAreas.Data.ToList().ForEach(area => response.AreaDetails.Add(new AreaDetail
            {
                Id = area.Id.ToString(),
                Name = area.Name,
                Description = area.Description,
                DistrictId = area.DistrictId.ToString(),
                AreaTypeName = area.AreaTypeName,
                EmailAddress = area.EmailAddress,
                FaxNumber = area.FaxNumber,
                GsmNumber = area.GsmNumber,
                Latitude = area.Latitude,
                Longitude = area.Longitude,
                PhoneNumber = area.PhoneNumber,
                PhoneNumber2 = area.PhoneNumber2,
                Street = area.Street,
                AreaTypeId = area.AreaTypeId,
                Audit = new Audit
                {
                    AuditCreateBy = area.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(area.AuditCreateDate),
                    AuditModifiedBy = area.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(area.AuditModifiedDate),
                }
            }));
            return response;
        }


        public override async Task<CreateSuccessResponse> AddArea(CreateAreaRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateArea
            {
                DistrictId = request._DistrictId,
                EmailAddress = request.EmailAddress,
                AreaTypeId = request.AreaTypeId,
                Description = request.Description,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude.HasValue ? request.Latitude.Value : 0,
                Longitude = request.Longitude.HasValue ? request.Longitude.Value : 0,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                PhoneNumber2 = request.PhoneNumber2,
                Street = request.Street,
            });

            return new CreateSuccessResponse { Created = "Created" };
        }

       
        public override async Task<UpdateSuccessResponse> ChangeArea(UpdateAreaRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateArea
            {
                EmailAddress = request.EmailAddress,
                AreaId = request._AreaId,
                Description = request.Description,
                PhoneNumber2 = request.PhoneNumber2,
                PhoneNumber = request.PhoneNumber,
                AreaTypeId = request.AreaTypId,
                FaxNumber = request.FaxNumber,
                GsmNumber = request.GsmNumber,
                Latitude = request.Latitude.HasValue ? request.Latitude.Value : 0,
                Longitude = request.Longitude.HasValue ? request.Longitude.Value : 0,
                Name = request.Name,
                Street = request.Street
            });

            return new UpdateSuccessResponse { Updated = "Updated" };
        }


        public override async Task<DeleteSuccessResponse> DeleteArea(DeleteAreaRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteArea { AreaId = request._AreaId });
            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }


        public override async Task<GetAreaTypesResponse> GetAreaTypes(GetAreaTypesRequest request, ServerCallContext context)
        {
            var areaTypes = await _queryDispatcher.QueryAsync(new GetAreaTypes { });

            GetAreaTypesResponse response = new();

            areaTypes.ToList().ForEach(type => response.AreaTypes.Add(new AreaType
            {
                Id = type.Id,
                Name = type.Name
            }));

            return response;
        }
    }
}
