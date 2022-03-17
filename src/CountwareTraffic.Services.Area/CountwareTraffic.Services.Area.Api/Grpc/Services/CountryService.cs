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
    public class CountryService : Country.CountryBase
    {
        private readonly ILogger<CountryService> _logger;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public CountryService(ILogger<CountryService> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        public override async Task<GetCountryDetailResponse> GetCountryById(GetCountryRequest request, ServerCallContext context)
        {
            var country = await _queryDispatcher.QueryAsync(new GetCountry { CountryId = request._CountryId });

            GetCountryDetailResponse response = new();

            response.CountryDetail = new()
            {
                Id = country.Id.ToString(),
                Name = country.Name,
                CompanyId = country.CompanyId.ToString(),
                Capital = country.Capital,
                ContinentCode = country.ContinentCode,
                CurrencyCode = country.CurrencyCode,
                Iso = country.Iso,
                Iso3 = country.Iso3,
                IsoNumeric = country.IsoNumeric,
                Audit = new Audit
                {
                    AuditCreateBy = country.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(country.AuditCreateDate),
                    AuditModifiedBy = country.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(country.AuditModifiedDate),
                }
            };
            return response;
        }

        public override async Task<CountryPagingResponse> GetCountries(GetCountriesRequest request, ServerCallContext context)
        {
            var pagingCountries = await _queryDispatcher.QueryAsync(new GetCountries
            {
                CompanyId = request._CompanyId,
                PagingQuery = new PagingQuery(request.PagingRequest.Page, request.PagingRequest.Limit)
            });

            CountryPagingResponse response = new()
            {
                TotalCount = pagingCountries.TotalCount,
                HasNextPage = pagingCountries.HasNextPage,
                Page = pagingCountries.Page,
                Limit = pagingCountries.Limit,
                Next = pagingCountries.Next,
                Prev = pagingCountries.Prev
            };

            pagingCountries.Data.ToList().ForEach(country => response.CountryDetails.Add(new CountryDetail
            {
                Id = country.Id.ToString(),
                Name = country.Name,
                CompanyId = country.CompanyId.ToString(),
                Capital = country.Capital,
                ContinentCode = country.ContinentCode,
                CurrencyCode = country.CurrencyCode,
                Iso = country.Iso,
                Iso3 = country.Iso3,
                IsoNumeric = country.IsoNumeric,
                Audit = new Audit
                {
                    AuditCreateBy = country.AuditCreateBy.ToString(),
                    AuditCreateDate = Timestamp.FromDateTimeOffset(country.AuditCreateDate),
                    AuditModifiedBy = country.AuditModifiedBy.ToString(),
                    AuditModifiedDate = Timestamp.FromDateTimeOffset(country.AuditModifiedDate),
                }
            }));
            return response;
        }

        public override async Task<CreateSuccessResponse> AddCountry(CreateCountryRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new CreateCountry
            {
                CompanyId = request._CompanyId,
                Name = request.Name,
                Capital = request.Capital,
                IsoNumeric = request.IsoNumeric.HasValue ? request.IsoNumeric.Value : 0,
                ContinentCode = request.ContinentCode,
                CurrencyCode = request.CurrencyCode,
                Iso = request.Iso,
                Iso3 = request.Iso3
            });

            return new CreateSuccessResponse { Created = "Created" };
        }

        public override async Task<UpdateSuccessResponse> ChangeCountry(UpdateCountryRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new UpdateCountry
            {
                 Capital = request.Capital,
                 ContinentCode = request.ContinentCode,
                 CountryId = request._CountryId,
                 CurrencyCode = request.CurrencyCode,
                 Iso = request.Iso,
                 Iso3 = request.Iso3,
                IsoNumeric = request.IsoNumeric.HasValue ? request.IsoNumeric.Value : 0,
            });

            return new UpdateSuccessResponse { Updated = "Updated" };
        }

        public override async Task<DeleteSuccessResponse> DeleteCountry(DeleteCountryRequest request, ServerCallContext context)
        {
            await _commandDispatcher.SendAsync(new DeleteCountry {   CountryId = request._CountryId });
            return new DeleteSuccessResponse { Deleted = "Deleted" };
        }
    }
}
