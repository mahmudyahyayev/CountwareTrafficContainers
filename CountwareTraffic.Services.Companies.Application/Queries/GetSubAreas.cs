using Convey.CQRS.Queries;
using System;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetSubAreas: IQuery<PagingResult<SubAreaDetailsDto>>
    {
        public Guid AreaId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
