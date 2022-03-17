using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class UpdateDistrict : ICommand
    {
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
    }
}
