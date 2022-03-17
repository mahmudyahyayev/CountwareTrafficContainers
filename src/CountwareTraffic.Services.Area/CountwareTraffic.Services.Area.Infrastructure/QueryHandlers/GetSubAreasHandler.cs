using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class GetSubAreasHandler : IQueryHandler<GetSubAreas, PagingResult<SubAreaDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubAreasHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResult<SubAreaDetailsDto>> HandleAsync(GetSubAreas query)
        {
            int page = query.PagingQuery.Page;
            int limit = query.PagingQuery.Limit;

            var qres = await _unitOfWork
                                        .GetRepository<ISubAreaRepository>()
                                        .GetAllAsync(page, limit, query.AreaId);

            if (qres == null)
                return PagingResult<SubAreaDetailsDto>.Empty;

            bool hasNextPage = qres.Total > (limit * (page - 1)) + qres.Entities.Count;

            return new PagingResult<SubAreaDetailsDto>(qres.Entities.Select(x => new SubAreaDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                AreaId = x.AreaId,
                AuditCreateBy = x.AuditCreateBy,
                AuditCreateDate = x.AuditCreateDate,
                AuditModifiedBy = x.AuditModifiedBy,
                AuditModifiedDate = x.AuditModifiedDate,
                Description = x.Description
            }), qres.Total, page, limit, hasNextPage);
        }
    }
}
