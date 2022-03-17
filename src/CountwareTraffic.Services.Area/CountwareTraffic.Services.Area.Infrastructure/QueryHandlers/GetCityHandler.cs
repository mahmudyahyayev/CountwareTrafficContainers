using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class GetCityHandler : IQueryHandler<GetCity, CityDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CityDetailsDto> HandleAsync(GetCity query)
        {
            var city = await _unitOfWork.GetRepository<ICityRepository>()
                .GetAsync(query.CityId);

            if (city == null)
                throw new CityNotFoundException(query.CityId);

            return new CityDetailsDto
            {
                Id = city.Id,
                Name = city.Name,
                AuditCreateBy = city.AuditCreateBy,
                AuditCreateDate = city.AuditCreateDate,
                AuditModifiedBy = city.AuditModifiedBy,
                AuditModifiedDate = city.AuditModifiedDate,
                CountryId = city.CountryId
            };
        }
    }
}
