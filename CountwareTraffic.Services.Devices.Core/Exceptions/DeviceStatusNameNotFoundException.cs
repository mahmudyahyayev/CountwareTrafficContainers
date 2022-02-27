using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceStatusNameNotFoundException : DomainException
    {
        IEnumerable<DeviceStatus> DeviceStatuses { get; }
        public string Name { get; }

        public DeviceStatusNameNotFoundException(IEnumerable<DeviceStatus> deviceStatuses, string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DeviceStatus Name: {String.Join(",", deviceStatuses.Select(s => s.Name))}") }, 400, ResponseMessageType.Error)
        {
            DeviceStatuses = deviceStatuses;
            Name = name;
        }
    }
}
