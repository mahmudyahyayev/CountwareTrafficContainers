using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class InvalidContinentCodeException : DomainException
    {
        public string ContinentCode { get; }
        public InvalidContinentCodeException(string continentCode)
            : base(new List<ErrorResult>() { new ErrorResult($"ContinentCode code must be 2 character. Current: {continentCode}") }, 400, ResponseMessageType.Error)
        {
            ContinentCode = continentCode;
        }
    }
}
