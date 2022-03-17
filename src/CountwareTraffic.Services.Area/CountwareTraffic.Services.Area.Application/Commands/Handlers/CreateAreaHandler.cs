using Convey.CQRS.Commands;
using CountwareTraffic.Services.Areas.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Application
{
    public class CreateAreaHandler : ICommandHandler<CreateArea>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateAreaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateArea command)
        {
            var areaRepository = _unitOfWork.GetRepository<IAreaRepository>();

            if (!await _unitOfWork.GetRepository<IDistrictRepository>().ExistsAsync(command.DistrictId))
                throw new DistrictNotFoundException(command.DistrictId);

            if (await areaRepository.ExistsAsync(command.Name))
                throw new AreaAlreadyExistsException(command.Name);

            var areaAddress = AreaAddress.Create(command.Street, command.Latitude, command.Longitude);

            var areaContract = AreaContact.Create(command.GsmNumber, command.PhoneNumber, command.PhoneNumber2, command.EmailAddress, command.FaxNumber);

            var area = CountwareTraffic.Services.Areas.Core.Area.Create(command.Name, command.Description, command.AreaTypeId, command.DistrictId, areaAddress, areaContract);

            await areaRepository.AddAsync(area);

            await _unitOfWork.CommitAsync();

        }
    }
}


