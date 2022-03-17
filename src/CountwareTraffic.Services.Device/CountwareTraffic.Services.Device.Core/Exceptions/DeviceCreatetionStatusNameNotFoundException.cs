using Mhd.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceCreationStatusNameNotFoundException : DomainException
    {
        IEnumerable<DeviceCreationStatus> DeviceCreationStatuses { get; }
        public string Name { get; }

        public DeviceCreationStatusNameNotFoundException(IEnumerable<DeviceCreationStatus> deviceCreationStatuses, string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DeviceCreatetionStatus Name: {String.Join(",", deviceCreationStatuses.Select(s => s.Name))}") }, 400, ResponseMessageType.Error)
        {
            DeviceCreationStatuses = deviceCreationStatuses;
            Name = name;
        }
    }
}
