using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class GetCountriesHandler : IQueryHandler<GetCountries, PagingResult<CountryDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCountriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<CountryDetailsDto>> HandleAsync(GetCountries query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<ICountryRepository>()
                                        .GetAllAsync(page, limit,query.CompanyId);

            if (qres == null)
                return PagingResult<CountryDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<CountryDetailsDto>(qres.Entities.Select(x => new CountryDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                AuditCreateBy = x.AuditCreateBy,
                AuditCreateDate = x.AuditCreateDate,
                AuditModifiedBy = x.AuditModifiedBy,
                AuditModifiedDate = x.AuditModifiedDate,
                Capital = x.Capital,
                CompanyId = x.CompanyId,
                ContinentCode = x.ContinentCode,
                CurrencyCode = x.CurrencyCode,
                Iso = x.Iso,
                Iso3 = x.Iso3,
                IsoNumeric = x.IsoNumeric
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}

