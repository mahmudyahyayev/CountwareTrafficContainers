using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class InvalidIsoCodeException : DomainException
    {
        public string Iso { get; }
        public InvalidIsoCodeException(string iso)
            : base(new List<ErrorResult>() { new ErrorResult($"Iso code must be 2 character. Current: {iso}") }, 400, ResponseMessageType.Error)
        {
            Iso = iso;
        }
    }
}
