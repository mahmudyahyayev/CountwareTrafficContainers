using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetDistrictHandler : IQueryHandler<GetDistrict, DistrictDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDistrictHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DistrictDetailsDto> HandleAsync(GetDistrict query)
        {
            var district = await _unitOfWork.GetRepository<IDistrictRepository>()
                .GetAsync(query.DistrictId);

            if (district == null)
                throw new DistrictNotFoundException(query.DistrictId);

            return new DistrictDetailsDto
            {
                Id = district.Id,
                Name = district.Name,
                AuditCreateBy = district.AuditCreateBy,
                AuditCreateDate = district.AuditCreateDate,
                AuditModifiedBy = district.AuditModifiedBy,
                AuditModifiedDate = district.AuditModifiedDate,
                CityId = district.CityId
            };
        }
    }
}
