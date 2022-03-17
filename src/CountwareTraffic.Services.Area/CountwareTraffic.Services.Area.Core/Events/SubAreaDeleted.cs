using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Areas.Core
{
    public class SubAreaDeleted : IDomainEvent
    {
        public Guid SubAreaId { get; init; }
        public Guid RecordId { get; init; }
        public SubAreaDeleted(Guid subAreaId)
        {
            SubAreaId = subAreaId;
            RecordId = Guid.NewGuid();
        }
    }
}
