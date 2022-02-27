using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class GetDevice : IQuery<DeviceDetailsDto>
    {
        public Guid DeviceId { get; set; }
    }
}
