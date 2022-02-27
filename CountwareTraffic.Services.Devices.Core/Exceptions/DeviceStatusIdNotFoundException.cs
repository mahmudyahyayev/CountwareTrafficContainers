using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceStatusIdNotFoundException : DomainException
    {
        IEnumerable<DeviceStatus> DeviceStatuses { get; }
        public int Id { get; }

        public DeviceStatusIdNotFoundException(IEnumerable<DeviceStatus> deviceStatuses, int id)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DeviceStatus Id: {String.Join(",", deviceStatuses.Select(s => s.Id))}") }, 400, ResponseMessageType.Error)
        {
            DeviceStatuses = deviceStatuses;
            Id = id;
        }
    }
}
