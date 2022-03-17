using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Core
{
    public class IpAddressIsEmptyOrWrongFormatException : DomainException
    {
        public string IpAddress { get; }

        public IpAddressIsEmptyOrWrongFormatException(string ipAddress)
            : base(new List<ErrorResult>() { new ErrorResult($"IpAddress with value: {ipAddress} wrong format.") }, 422, ResponseMessageType.Error)
        {
            IpAddress = ipAddress;
        }
    }
}
