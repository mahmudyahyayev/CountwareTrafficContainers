using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Application
{
    public class GetSubArea : IQuery<SubAreaDetailsDto>
    {
        public Guid SubAreaId { get; set; }
    }
}
