using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public class CreateSubArea : ICommand
    {
        public Guid AreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
