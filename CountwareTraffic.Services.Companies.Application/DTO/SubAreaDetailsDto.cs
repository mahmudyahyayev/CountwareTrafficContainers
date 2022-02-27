using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class SubAreaDetailsDto : SubAreaDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AreaId { get; set; }
    }
}
