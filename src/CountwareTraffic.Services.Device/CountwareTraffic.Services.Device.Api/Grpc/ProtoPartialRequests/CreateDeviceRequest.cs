using Mhd.Framework.Core;
using System;
using System.Net;

namespace CountwareTraffic.Services.Devices.Grpc
{
    [ServiceLog]
    public sealed partial class CreateDeviceRequest : RequestValidate
    {
        internal Guid _SubAreaId
        {
            get
            {
                if (Guid.TryParse(subAreaId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { subAreaId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_SubAreaId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"SubAreaId cannot be null", nameof(_SubAreaId)));

            if (String.IsNullOrEmpty(this.Name))
                ValidateResults.Add(new ErrorResult($"Name Value cannot be null", nameof(Name)));

            if (String.IsNullOrEmpty(this.Model))
                ValidateResults.Add(new ErrorResult($"Model Value cannot be null", nameof(Model)));

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