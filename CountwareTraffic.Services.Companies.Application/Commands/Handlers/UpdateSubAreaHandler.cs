using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class UpdateSubAreaHandler : ICommandHandler<UpdateSubArea>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSubAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateSubArea command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            var subArea = await subAreaRepository.GetAsync(command.SubAreaId);

            if (subArea is null)
                throw new SubAreaNotFoundException(command.SubAreaId);

            if (subArea.Name == command.Name)
            {
                if (subArea.Description == command.Description)
                    return;

                subArea.WhenChanged(command.Name, command.Description);

                await _unitOfWork.CommitAsync();
                return;
            }
            
                
            if(await subAreaRepository.ExistsAsync(command.Name))
                throw new SubAreaAlreadyExistsException(command.Name);

            subArea.WhenChanged(command.Name, command.Description);

            

            await _unitOfWork.CommitAsync();
        }
    }
}
