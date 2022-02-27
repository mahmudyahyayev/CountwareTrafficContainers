using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetCountryHandler : IQueryHandler<GetCountry, CountryDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CountryDetailsDto> HandleAsync(GetCountry query)
        {
            var country = await _unitOfWork.GetRepository<ICountryRepository>()
                .GetAsync(query.CountryId);

            if (country == null)
                throw new CountryNotFoundException(query.CountryId);

            return new CountryDetailsDto
            {
                Id = country.Id,
                Name = country.Name,
                AuditCreateBy = country.AuditCreateBy,
                AuditCreateDate = country.AuditCreateDate,
                AuditModifiedBy = country.AuditModifiedBy,
                AuditModifiedDate = country.AuditModifiedDate,
                Capital = country.Capital,
                CompanyId = country.CompanyId,
                ContinentCode = country.ContinentCode,
                CurrencyCode = country.CurrencyCode,
                Iso = country.Iso,
                Iso3 = country.Iso3,
                IsoNumeric = country.IsoNumeric
            };
        }
    }
}