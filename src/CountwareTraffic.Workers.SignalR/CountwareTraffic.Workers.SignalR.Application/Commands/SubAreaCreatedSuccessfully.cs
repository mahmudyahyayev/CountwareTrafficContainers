using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Workers.SignalR.Application
{
    [Contract]
    public class SubAreaCreatedSuccessfully : ICommand
    {
        public Guid SubAreaId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string SubAreaStatus { get; set; }
    }
}
