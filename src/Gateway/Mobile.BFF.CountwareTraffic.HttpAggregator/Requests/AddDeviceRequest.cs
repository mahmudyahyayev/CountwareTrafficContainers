using Mhd.Framework.Core;
using System;
using System.Net;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class AddDeviceRequest : RequestValidate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public int DeviceTypeId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Identity { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public string MacAddress { get; set; }
        public string UniqueValue { get; set; }

        public override void Validate()
        {
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
