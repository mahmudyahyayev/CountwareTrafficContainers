using Convey.CQRS.Queries;
using CountwareTraffic.Services.Areas.Application;
using CountwareTraffic.Services.Areas.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Areas.Infrastructure
{
    public class GetAreaTypesHandler : IQueryHandler<GetAreaTypes, IEnumerable<AreaTypeDto>>
    {
        public async Task<IEnumerable<AreaTypeDto>> HandleAsync(GetAreaTypes query)
        {
            return AreaType.List().Select(type => new AreaTypeDto { Id = type.Id, Name = type.Name });
        }
    }
}
