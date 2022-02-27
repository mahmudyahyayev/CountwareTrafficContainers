using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public class CreateDistrict : ICommand
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }
}
