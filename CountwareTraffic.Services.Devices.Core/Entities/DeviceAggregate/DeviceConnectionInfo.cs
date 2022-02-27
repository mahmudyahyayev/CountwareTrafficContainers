using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Core
{
    public record DeviceConnectionInfo
    {
        private string _ipAddress;
        private int _port;
        private string _identity;
        private string _password;
        private string _uniqueId;
        private string _macAddress;

        public string IpAddress => _ipAddress;
        public int Port => _port;
        public string Identity => _identity;
        public string Password => _password;
        public string UniqueId => _uniqueId;
        public string MacAddress => _macAddress;

        private DeviceConnectionInfo() { }

        public static DeviceConnectionInfo Create(string ipAddress, int port, string identity, string password, string uniqueId, string macAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress ipA))
                throw new IpAddressIsEmptyOrWrongFormatException(ipAddress);

            if (string.IsNullOrEmpty(macAddress))
                throw new ArgumentNullException(nameof(macAddress));

            if (port < 1 || port > 65535)
                throw new InvalidPortNumberException(port);

            return new DeviceConnectionInfo
            {
                _ipAddress = ipAddress,
                _port = port,
                _identity = identity,
                _password = password,
                _uniqueId = uniqueId,
                _macAddress = macAddress
            };
        }
    }
}
