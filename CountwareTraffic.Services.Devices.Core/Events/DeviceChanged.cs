using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceChanged : IDomainEvent
    {
        public Guid DeviceId { get; init; }
        public string Name { get; init; }
        public string OldName { get; init; }
        public string queueName { get; set; }
        public Guid RecordId { get ; init; }

        public DeviceChanged(Guid deviceId, string name, string oldName)
        {
            DeviceId = deviceId;
            Name = name;
            OldName = oldName;
            RecordId = Guid.NewGuid();
        }
    }
}
