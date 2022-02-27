using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    [Contract]
    public class DeleteCompany : ICommand
    {
        public Guid CompanyId { get; set; }
    }
}
