using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
{
    [Contract]
    public class DeviceChangedFailed : ICommand
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public Guid RecordId { get; init; }
        public string UserName { get; set; }
    }
}
