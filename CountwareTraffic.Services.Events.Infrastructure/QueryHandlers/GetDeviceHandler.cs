using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class GetDeviceHandler : IQueryHandler<GetDevice, DeviceDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDevicesHandler> _logger;
        public GetDeviceHandler(IUnitOfWork unitOfWork, ILogger<GetDevicesHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DeviceDto> HandleAsync(GetDevice query)
        {
            var device = await _unitOfWork
                                        .GetRepository<IDeviceRepository>()
                                        .GetByNameAsync(query.DeviceName);

            if (device == null)
                return default;

            return new DeviceDto
            {
                Id = device.Id,
                Name = device.Name
            };
        }
    }
}
