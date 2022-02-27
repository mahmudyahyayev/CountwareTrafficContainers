using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public  class DeleteCity : ICommand
    {
        public Guid CityId { get; set; }
    }
}
