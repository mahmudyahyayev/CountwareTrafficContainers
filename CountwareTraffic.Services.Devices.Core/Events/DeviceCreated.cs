using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceCreated : IDomainEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public Guid RecordId { get; init; }

        public DeviceCreated(Guid deviceId, string name)
        {
            DeviceId = deviceId;
            Name = name;
            RecordId = Guid.NewGuid();
        }
    }
}
