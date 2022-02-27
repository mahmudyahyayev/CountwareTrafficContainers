using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public class DeleteCountry : ICommand
    {
        public Guid CountryId { get; set; }
    }
}
