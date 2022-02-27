using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class ChangeAreaTypeHandler : ICommandHandler<ChangeAreaType>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeAreaTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(ChangeAreaType command)
        {
           var areaRepository =  _unitOfWork.GetRepository<IAreaRepository>();

            var area = await areaRepository.GetAsync(command.AreaId);

            if (area is null)
                throw new AreaNotFoundException(command.AreaId);

            area.ChangeAreaType(command.AreaTypeId);

            await _unitOfWork.CommitAsync();
        }
    }
}