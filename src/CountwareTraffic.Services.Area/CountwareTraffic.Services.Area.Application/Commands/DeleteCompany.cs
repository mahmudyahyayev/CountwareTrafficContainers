using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class DeleteCompany : ICommand
    {
        public Guid CompanyId { get; set; }
    }
}
