using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Workers.SignalR.Application
{
    [Contract]
    public class DeviceDeletedSuccessfully : ICommand
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
