using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Areas.Core
{
    public class AreaTypeChanged : IDomainEvent
    {
        public Area Area { get; }
        public int AreaTypeId { get; }
        public Guid RecordId { get ; init; }

        public AreaTypeChanged(Area area, int areaTypeId)
        {
            Area = area;
            AreaTypeId = areaTypeId;
            RecordId = Guid.NewGuid();
        }
    }
}
