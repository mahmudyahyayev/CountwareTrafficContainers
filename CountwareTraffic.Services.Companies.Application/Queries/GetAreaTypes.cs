using Convey.CQRS.Queries;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetAreaTypes : IQuery<IEnumerable<AreaTypeDto>>
    {
    }
}
