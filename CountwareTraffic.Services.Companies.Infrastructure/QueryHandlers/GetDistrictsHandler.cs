using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetDistrictsHandler : IQueryHandler<GetDistricts, PagingResult<DistrictDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDistrictsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<DistrictDetailsDto>> HandleAsync(GetDistricts query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<IDistrictRepository>()
                                        .GetAllAsync(page, limit, query.CityId);

            if (qres == null)
                return PagingResult<DistrictDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<DistrictDetailsDto>(qres.Entities.Select(x => new DistrictDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CityId = x.CityId,
                AuditCreateBy = x.AuditCreateBy,
                AuditCreateDate = x.AuditCreateDate,
                AuditModifiedBy = x.AuditModifiedBy,
                AuditModifiedDate = x.AuditModifiedDate,
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}