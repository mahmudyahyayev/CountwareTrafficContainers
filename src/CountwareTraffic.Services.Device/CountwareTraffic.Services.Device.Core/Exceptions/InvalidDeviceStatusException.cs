using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Core
{
    public class InvalidDeviceStatusException : DomainException
    {
        public int DeviceStatus { get; }

        public InvalidDeviceStatusException(int deviceStatusId)
            : base(new List<ErrorResult>() { new ErrorResult($"DeviceStatusId with id: {deviceStatusId} invalid format.") }, 400, ResponseMessageType.Error)
        {
            DeviceStatus = deviceStatusId;
        }
    }
}
