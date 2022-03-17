using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Areas.Application
{
    [Contract]
    public class ChangeAreaType : ICommand
    {
        public Guid AreaId { get; }
        public int AreaTypeId { get; }

        public ChangeAreaType(Guid areaId, int areaTypeId)
        {
            AreaId = areaId;
            AreaTypeId = areaTypeId;
        }
    }
}
