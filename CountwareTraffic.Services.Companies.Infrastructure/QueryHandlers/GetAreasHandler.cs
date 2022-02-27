using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetAreasHandler : IQueryHandler<GetAreas, PagingResult<AreaDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAreasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<AreaDetailsDto>> HandleAsync(GetAreas query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<IAreaRepository>()
                                        .GetAllAsync(page, limit, query.DistrictId);

            if (qres == null)
                return PagingResult<AreaDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<AreaDetailsDto>(qres.Entities.Select(area => new AreaDetailsDto
            {
                Id = area.Id,
                AreaTypeName = area.AreaType.Name,
                AreaTypeId = area.AreaType.Id,
                Name = area.Name,
                Description = area.Description,
                DistrictId = area.DistrictId,
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
                Street = area.Address.Street
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}
