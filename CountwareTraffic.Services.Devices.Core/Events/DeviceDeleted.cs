using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceDeleted : IDomainEvent
    {
        public Guid DeviceId { get; init; }
        public Guid RecordId { get; init; }
        public string Name { get; init; }
        public DeviceDeleted(Guid deviceId,  string name)
        {
            DeviceId = deviceId;
            RecordId = Guid.NewGuid();
            Name = name;
        }
    }
}
