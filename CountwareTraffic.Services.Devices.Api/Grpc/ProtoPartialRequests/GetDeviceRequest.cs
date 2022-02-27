using Sensormatic.Tool.Core;
using System;

namespace CountwareTraffic.Services.Devices.Grpc
{
    [ServiceLog]
    public sealed partial class GetDeviceRequest : SensormaticRequestValidate
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
        }
    }
}
