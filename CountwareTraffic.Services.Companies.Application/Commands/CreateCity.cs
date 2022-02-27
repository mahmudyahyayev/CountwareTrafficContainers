using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public class CreateCity : ICommand
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
    }
}
