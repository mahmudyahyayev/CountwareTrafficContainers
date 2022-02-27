using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Companies.Core
{
    public class SubAreaCreated : IDomainEvent
    {
        public Guid SubAreaId { get; init; }
        public string  Name { get; init; }
        public Guid RecordId { get; init; }

        public SubAreaCreated(Guid subAreaId, string name)
        {
            SubAreaId = subAreaId;
            Name = name;
            RecordId = Guid.NewGuid();
        }
    }
}
