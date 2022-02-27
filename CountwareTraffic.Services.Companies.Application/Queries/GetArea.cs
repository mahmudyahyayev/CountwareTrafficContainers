using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetArea : IQuery<AreaDetailsDto>
    {
        public Guid AreaId { get; set; }
    }
}
