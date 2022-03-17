using System;

namespace CountwareTraffic.Services.Devices.Application
{
    public class DeviceDetailsDto : DeviceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public Guid SubAreaId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Identity { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public string MacAddress { get; set; }
        public string DeviceStatusName { get; set; }
        public string DeviceTypeName { get; set; }
        public int DeviceStatusId { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
