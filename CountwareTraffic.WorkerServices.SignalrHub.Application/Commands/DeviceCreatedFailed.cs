using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    [Contract]
    public class DeviceCreatedFailed : ICommand
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DeviceCreationStatus { get; set; }
    }
}
