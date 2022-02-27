using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetAreaHandler : IQueryHandler<GetArea, AreaDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AreaDetailsDto> HandleAsync(GetArea query)
        {
            var area = await _unitOfWork.GetRepository<IAreaRepository>()
                .GetAsync(query.AreaId);

            if (area == null)
                throw new AreaNotFoundException(query.AreaId);

            return new AreaDetailsDto
            {
                Id = area.Id,
                Name = area.Name,
                Description = area.Description,
                AuditCreateBy = area.AuditCreateBy,
                AuditCreateDate = area.AuditCreateDate,
                AuditModifiedBy = area.AuditModifiedBy,
                AuditModifiedDate = area.AuditModifiedDate,
                EmailAddress = area.Contact.EmailAddress,
                FaxNumber = area.Contact.FaxNumber,
                GsmNumber = area.Contact.GsmNumber,
                PhoneNumber = area.Contact.PhoneNumber,
                PhoneNumber2 = area.Contact.PhoneNumber2,
                Latitude = area.Address.Location?.Y,
                Longitude = area.Address.Location?.X,
                Street = area.Address.Street,
                AreaTypeName = area.AreaType.Name,
                AreaTypeId = area.AreaType.Id,
                DistrictId = area.DistrictId
            };
        }
    }
}
