using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class DeleteDistrict : ICommand
    {
        public Guid DistrictId { get; set; }
    }
}
