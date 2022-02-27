using Convey.CQRS.Queries;
using CountwareTraffic.Services.Events.Application;
using CountwareTraffic.Services.Events.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class GetDevicesHandler : IQueryHandler<GetDevices, IList<DeviceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetDevicesHandler> _logger;
        public GetDevicesHandler(IUnitOfWork unitOfWork, ILogger<GetDevicesHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IList<DeviceDto>> HandleAsync(GetDevices query)
        {
            var qres = await _unitOfWork
                                        .GetRepository<IDeviceRepository>()
                                        .GetAsync();

            if (qres == null)
                return default;

            return new List<DeviceDto>(qres.Select(device => new DeviceDto
            {
                Id = device.Id,
                Name = device.Name,
            }));
        }
    }
}
