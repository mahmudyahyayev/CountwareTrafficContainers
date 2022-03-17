using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class GetSubAreaHandler : IQueryHandler<GetSubArea, SubAreaDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SubAreaDetailsDto> HandleAsync(GetSubArea query)
        {
            var subArea = await _unitOfWork.GetRepository<ISubAreaRepository>()
                .GetAsync(query.SubAreaId);

            if (subArea == null)
                throw new SubAreaNotFoundException(query.SubAreaId);

            return new SubAreaDetailsDto
            {
                Id = subArea.Id,
                Name = subArea.Name,
                AuditCreateBy = subArea.AuditCreateBy,
                AuditCreateDate = subArea.AuditCreateDate,
                AuditModifiedBy = subArea.AuditModifiedBy,
                AuditModifiedDate = subArea.AuditModifiedDate,
                AreaId = subArea.AreaId,
                Description = subArea.Description
            };
        }
    }
}
