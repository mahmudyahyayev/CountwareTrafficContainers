using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class GetCitiesHandler : IQueryHandler<GetCities, PagingResult<CityDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCitiesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<CityDetailsDto>> HandleAsync(GetCities query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<ICityRepository>()
                                        .GetAllAsync(page, limit, query.CountryId);

            if (qres == null)
                return PagingResult<CityDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<CityDetailsDto>(qres.Entities.Select(x => new CityDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
                AuditCreateBy = x.AuditCreateBy,
                AuditCreateDate = x.AuditCreateDate,
                AuditModifiedBy = x.AuditModifiedBy,
                AuditModifiedDate = x.AuditModifiedDate,
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}
