using Convey.CQRS.Queries;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Application
{
    public class GetAreaTypes : IQuery<IEnumerable<AreaTypeDto>>
    {
    }
}
