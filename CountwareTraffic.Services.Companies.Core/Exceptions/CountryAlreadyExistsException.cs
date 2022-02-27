﻿using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class CountryAlreadyExistsException : DomainException
    {
        public string CountryName { get; }

        public CountryAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Country with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            CountryName = name;
        }
    }
}
