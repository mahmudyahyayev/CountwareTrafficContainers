using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Workers.SignalR.Application
{
    [Contract]
    public class SubAreaChangedFailed : ICommand
    {
        public Guid SubAreaId { get; set; }
        public Guid UserId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public Guid RecordId { get; init; }
        public string UserName { get; set; }
    }
}
