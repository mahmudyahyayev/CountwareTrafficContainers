using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class UpdateSubArea : ICommand
    {
        public Guid SubAreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
