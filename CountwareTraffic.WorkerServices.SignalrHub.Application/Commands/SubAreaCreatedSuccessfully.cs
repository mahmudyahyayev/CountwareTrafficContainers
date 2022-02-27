using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.WorkerServices.SignalrHub.Application
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
