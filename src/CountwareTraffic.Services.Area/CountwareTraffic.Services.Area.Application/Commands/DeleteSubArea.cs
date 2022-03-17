using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class DeleteSubArea : ICommand
    {
        public Guid SubAreaId { get; set; }
    }
}
