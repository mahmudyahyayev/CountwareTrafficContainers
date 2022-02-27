using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public class DeleteArea : ICommand
    {
        public Guid AreaId { get; set; }
    }
}
