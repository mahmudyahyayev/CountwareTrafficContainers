using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class CompanyNotFoundException : DomainException
    {
        public Guid CompanyId { get; }

        public CompanyNotFoundException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"Company with id: {id} not found.") }, 404, ResponseMessageType.Error)
        {
            CompanyId = id;
        }
    }
}
