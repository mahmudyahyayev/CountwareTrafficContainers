using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class CompanyAlreadyExistsException : DomainException
    {
        public string CompanyName { get; }

        public CompanyAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Company with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            CompanyName = name;
        }
    }
}
