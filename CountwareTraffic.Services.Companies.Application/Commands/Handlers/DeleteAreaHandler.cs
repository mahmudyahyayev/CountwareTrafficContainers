using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class DeleteAreaHandler : ICommandHandler<DeleteArea>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteArea command)
        {
            var areaRepository = _unitOfWork.GetRepository<IAreaRepository>();

            var area = await areaRepository.GetAsync(command.AreaId);

            if (area is null) throw new AreaNotFoundException(command.AreaId);

            areaRepository.Remove(area);

            await _unitOfWork.CommitAsync();
        }
    }
}
