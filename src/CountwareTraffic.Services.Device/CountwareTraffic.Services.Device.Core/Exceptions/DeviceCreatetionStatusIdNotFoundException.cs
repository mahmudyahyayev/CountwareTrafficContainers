using Mhd.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceCreationStatusIdNotFoundException : DomainException
    {
        IEnumerable<DeviceCreationStatus> DeviceCreationStatuses { get; }
        public int Id { get; }

        public DeviceCreationStatusIdNotFoundException(IEnumerable<DeviceCreationStatus> deviceCreationStatuses, int id)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DeviceCreatetionStatus Id: {String.Join(",", deviceCreationStatuses.Select(s => s.Id))}") }, 400, ResponseMessageType.Error)
        {
            DeviceCreationStatuses = deviceCreationStatuses;
            Id = id;
        }
    }
}
