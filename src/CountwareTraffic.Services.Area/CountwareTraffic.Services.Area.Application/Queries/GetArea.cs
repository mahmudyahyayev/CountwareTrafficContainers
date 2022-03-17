using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetArea : IQuery<AreaDetailsDto>
    {
        public Guid AreaId { get; set; }
    }
}
