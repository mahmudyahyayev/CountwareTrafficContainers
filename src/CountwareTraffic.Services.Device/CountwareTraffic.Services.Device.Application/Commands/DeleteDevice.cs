using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Devices.Application
{
    [Contract]
    public class DeleteDevice : ICommand
    {
        public Guid DeviceId { get; set; }
    }
}
