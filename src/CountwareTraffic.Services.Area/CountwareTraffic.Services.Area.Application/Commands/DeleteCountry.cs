using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class DeleteCountry : ICommand
    {
        public Guid CountryId { get; set; }
    }
}
