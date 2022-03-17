using Convey.CQRS.Commands;
using CountwareTraffic.Services.Events.Core;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Application
{
    public class CreateEventHandler : ICommandHandler<CreateEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        public CreateEventHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _identityService = identityService;
        }

        public async Task HandleAsync(CreateEvent command)
        {
            var eventRepository = _unitOfWork.GetRepository<IEventRepository>();

            var deviceRepository = _unitOfWork.GetRepository<IDeviceRepository>();

            var device = await deviceRepository.GetAsync(command.DeviceId);

            if (device == null)
                command.DeviceId = System.Guid.Empty;

            var createBy = _identityService.UserId;

            var @event = Event.Create(command.Description, command.EventDate, command.DeviceId, command.DirectionTypeId, createBy);

            await eventRepository.AddAsync(@event);

            @event.CompleteCreation(device.Name);

            await _unitOfWork.CommitAsync();
        }
    }
}
