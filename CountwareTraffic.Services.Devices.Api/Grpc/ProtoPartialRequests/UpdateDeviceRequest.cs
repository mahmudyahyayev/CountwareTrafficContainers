using Sensormatic.Tool.Core;
using System;
using System.Net;

namespace CountwareTraffic.Services.Devices.Grpc
{
    [ServiceLog]
    public sealed partial class UpdateDeviceRequest : SensormaticRequestValidate
    {
        internal Guid _DeviceId
        {
            get
            {
                if (Guid.TryParse(deviceId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { deviceId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_DeviceId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"DeviceId cannot be null", nameof(_DeviceId)));

            if (String.IsNullOrEmpty(this.Name))
                ValidateResults.Add(new ErrorResult($"Name Value cannot be null", nameof(Name)));

            if (String.IsNullOrEmpty(this.Model))
                ValidateResults.Add(new ErrorResult($"Model Value cannot be null", nameof(Model)));

            if (DeviceStatusId < 1)
                ValidateResults.Add(new ErrorResult($"DeviceStatusId with id: {DeviceStatusId} invalid format", nameof(DeviceStatusId)));

            if (DeviceTypeId < 1)
                ValidateResults.Add(new ErrorResult($"DeviceTypeId with id: {DeviceTypeId} invalid format", nameof(DeviceTypeId)));

            if (!IPAddress.TryParse(IpAddress, out IPAddress ipA))
                ValidateResults.Add(new ErrorResult($"IpAddress with value: {IpAddress} wrong format", nameof(IpAddress)));

            if (string.IsNullOrEmpty(MacAddress))
                ValidateResults.Add(new ErrorResult($"MacAddress Value cannot be null", nameof(MacAddress)));

            if (Port < 1 || Port > 65535)
                ValidateResults.Add(new ErrorResult($"Port with value: {Port} wrong format", nameof(Port)));
        }
    }
}