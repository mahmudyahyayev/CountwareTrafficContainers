using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class CountryNotFoundException : DomainException
    {
        public Guid CountryId { get; }

        public CountryNotFoundException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"Country with id: {id} not found.") }, 404, ResponseMessageType.Error)
        {
            CountryId = id;
        }
    }
}
