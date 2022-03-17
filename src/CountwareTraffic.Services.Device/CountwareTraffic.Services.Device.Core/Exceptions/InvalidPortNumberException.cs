using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Core
{
    public class InvalidPortNumberException : DomainException
    {
        public int Port { get; }

        public InvalidPortNumberException(int port)
           : base(new List<ErrorResult>() { new ErrorResult($"Port with value: {port} invalid format.") }, 422, ResponseMessageType.Error)
        {
            Port = port;
        }
    }
}
