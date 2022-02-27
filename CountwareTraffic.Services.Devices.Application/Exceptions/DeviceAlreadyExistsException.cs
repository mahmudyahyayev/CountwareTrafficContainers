using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceAlreadyExistsException : AppException
    {
        public string DeviceName { get; }

        public DeviceAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Device with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            DeviceName = name;
        }
    }
}
