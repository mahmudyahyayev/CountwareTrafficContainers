using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Companies.Core
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
