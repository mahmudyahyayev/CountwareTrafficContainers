using Convey.CQRS.Queries;
using CountwareTraffic.Services.Companies.Application;
using CountwareTraffic.Services.Companies.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Infrastructure
{
    public class GetAreaTypesHandler : IQueryHandler<GetAreaTypes, IEnumerable<AreaTypeDto>>
    {
        public async Task<IEnumerable<AreaTypeDto>> HandleAsync(GetAreaTypes query)
        {
            return AreaType.List().Select(type => new AreaTypeDto { Id = type.Id, Name = type.Name });
        }
    }
}
