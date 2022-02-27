using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceTypeNameNotFoundException : DomainException
    {
        IEnumerable<DeviceType> DeviceTypes { get; }
        public string Name { get; }

        public DeviceTypeNameNotFoundException(IEnumerable<DeviceType> deviceTypes, string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DeviceType Name: {String.Join(",", deviceTypes.Select(s => s.Name))}") }, 400, ResponseMessageType.Error)
        {
            DeviceTypes = deviceTypes;
            Name = name;
        }
    }
}
