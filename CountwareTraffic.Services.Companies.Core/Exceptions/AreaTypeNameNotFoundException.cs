using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Core
{
    public class AreaTypeNameNotFoundException : DomainException
    {
        IEnumerable<AreaType> AreaTypes { get; }
        public string Name { get; }

        public AreaTypeNameNotFoundException(IEnumerable<AreaType> areaTypes, string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for AreaType Name: {String.Join(",", areaTypes.Select(s => s.Name))}") }, 400, ResponseMessageType.Error)
        {
            AreaTypes = areaTypes;
            Name = name;
        }
    }
}
