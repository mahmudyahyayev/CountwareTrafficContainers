using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class CityAlreadyExistsException : DomainException
    {
        public string CityName { get; }

        public CityAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"City with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            CityName = name;
        }
    }
}
