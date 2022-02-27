using Convey.CQRS.Commands;
using CountwareTraffic.Services.Companies.Core;
using System;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class CreateSubAreaHandler : ICommandHandler<CreateSubArea>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubAreaHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task HandleAsync(CreateSubArea command)
        {
            var subAreaRepository = _unitOfWork.GetRepository<ISubAreaRepository>();

            if (!await _unitOfWork.GetRepository<IAreaRepository>().ExistsAsync(command.AreaId))
                throw new AreaNotFoundException(command.AreaId);

            if ((await subAreaRepository.ExistsAsync(command.Name)))
                throw new SubAreaAlreadyExistsException(command.Name);

            var subArea = SubArea.Create(command.Name, command.Description, command.AreaId);

            await subAreaRepository.AddAsync(subArea);

            subArea.WhenCreated(subArea.Id, subArea.Name);

            await _unitOfWork.CommitAsync();
        }
    }
}
