using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetAreas : IQuery<PagingResult<AreaDetailsDto>>
    {
        public Guid DistrictId { get; set; }
        public PagingQuery PagingQuery { get; set; }
    }
}
