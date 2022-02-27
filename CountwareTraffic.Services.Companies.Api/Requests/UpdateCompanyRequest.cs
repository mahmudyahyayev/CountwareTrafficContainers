using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Api
{
    public class UpdateCompanyRequest : SensormaticRequestValidate
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));
        }
    }
}