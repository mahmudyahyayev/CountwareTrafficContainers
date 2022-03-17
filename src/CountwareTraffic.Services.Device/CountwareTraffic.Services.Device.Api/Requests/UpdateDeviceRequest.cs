using Mhd.Framework.Core;

namespace CountwareTraffic.Services.Devices.Api
{
    public class UpdateDeviceRequest : SensormaticRequestValidate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public int DeviceStatusId { get; set; }
        public int DeviceTypeId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Identity { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public string MacAddress { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));

            if (string.IsNullOrEmpty(Model))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Model)));

            if (string.IsNullOrEmpty(IpAddress))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(IpAddress)));

            if (Port == 0)
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Port)));

            if (string.IsNullOrEmpty(Identity))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Identity)));

            if (string.IsNullOrEmpty(Password))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Password)));

            if (string.IsNullOrEmpty(MacAddress))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(MacAddress)));
        }
    }
}
