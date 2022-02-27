using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class InvalidIso3CodeException : DomainException
    {
        public string Iso3 { get; }
        public InvalidIso3CodeException(string iso3)
            : base(new List<ErrorResult>() { new ErrorResult($"Iso3 code must be 3 character. Current: {iso3}") }, 400, ResponseMessageType.Error)
        {
            Iso3 = iso3;
        }
    }
}
