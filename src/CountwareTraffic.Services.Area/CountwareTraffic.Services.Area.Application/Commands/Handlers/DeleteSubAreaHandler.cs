using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class DeleteSubAreaHandler : ICommandHandler<DeleteSubArea>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteSubArea command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(command.SubAreaId);

            if (subArea is null)
                throw new SubAreaNotFoundException(command.SubAreaId);

            subAreaRepository.Remove(subArea);

            subArea.WhenDeleted(command.SubAreaId);

            await _unitOfWork.CommitAsync();
        }
    }
}
