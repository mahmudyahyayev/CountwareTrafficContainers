using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class AreaAlreadyExistsException : DomainException
    {
        public string AreaName { get; }

        public AreaAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Area with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            AreaName = name;
        }
    }
}