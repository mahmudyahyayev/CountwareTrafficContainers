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
    public class CityService : City.CityBase
    {
        private readonly ILogger<CityService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public CityService(ILogger<CityService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public override async Task<GetCityDetailResponse> GetCityById(GetCityRequest request, ServerCallContext context)
        {
            var city = await _queryDispatcher.QueryAsync(new GetCity { CityId = request._CityId });

            GetCityDetailResponse response = new();

            response.CityDetail = new()
            {
                CountryId = city.CountryId.ToString(),
                Id = city.Id.ToString(),
                Name = city.Name,
                Audit = new Audit
                {
                    AuditCreateBy = city.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(city.AuditCreateDate),
                    AuditModifiedBy = city.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(city.AuditModifiedDate),
                }
            };

            return response;
        }

        public override async Task<CityPagingResponse> GetCities(GetCitiesRequest request, ServerCallContext context)
        {
            var pagingCities = await _queryDispatcher.QueryAsync(new GetCities
            {
                CountryId = request._CountryId,
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            CityPagingResponse response = new()
            {
                TotalCount = pagingCities.TotalCount,
                HasNextPage = pagingCities.HasNextPage,
                Page = pagingCities.Page,
                Limit = pagingCities.Limit,
                Next = pagingCities.Next,
                Prev = pagingCities.Prev
            };

            pagingCities.Data.ToList().ForEach(city => response.CityDetails.Add(new CityDetail
            {
                Id = city.Id.ToString(),
                Name = city.Name,
                CountryId = city.CountryId.ToString(),
                Audit = new Audit
                {
                    AuditCreateBy = city.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(city.AuditCreateDate),
                    AuditModifiedBy = city.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(city.AuditModifiedDate),
                }
            }));

            return response;
        }

        public override async Task<CreateSuccessResponse> AddCity(CreateCityRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateCity
            {
                CountryId = request._CountryId,
                Name = request.Name,
            });

            return new CreateSuccessResponse { Created = "Created" };
        }

        public override async Task<UpdateSuccessResponse> ChangeCity(UpdateCityRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateCity
            {
                Name = request.Name,
                CityId = request._CityId
            });

            return new UpdateSuccessResponse { Updated = "Updated" };
        }

        public override async Task<DeleteSuccessResponse> DeleteCity(DeleteCityRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteCity { CityId = request._CityId });
            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }
    }
}
