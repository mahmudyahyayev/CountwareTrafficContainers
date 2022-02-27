using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Core
{
    public class InvalidDeviceTypeException : DomainException
    {
        public int DeviceType { get; }

        public InvalidDeviceTypeException(int deviceType)
            : base(new List<ErrorResult>() { new ErrorResult($"DeviceTypeId with id: {deviceType} invalid format.") }, 400, ResponseMessageType.Error)
        {
            DeviceType = deviceType;
        }
    }
}
