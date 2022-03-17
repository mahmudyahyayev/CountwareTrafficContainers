using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Workers.SignalR.Application
{
    [Contract]
    public class DeviceChangedSuccessfully : ICommand
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public Guid RecordId { get; init; }
        public string UserName { get; set; }
    }
}
