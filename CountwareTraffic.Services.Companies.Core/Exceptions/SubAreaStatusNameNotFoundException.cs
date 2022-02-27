using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Core
{
    public class SubAreaStatusNameNotFoundException : DomainException
    {
        IEnumerable<SubAreaStatus> SubAreaStatuses { get; }
        public string Name { get; }

        public SubAreaStatusNameNotFoundException(IEnumerable<SubAreaStatus> subAreaStatuses, string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for SubAreaStatus Name: {String.Join(",", subAreaStatuses.Select(s => s.Name))}") }, 400, ResponseMessageType.Error)
        {
            SubAreaStatuses = subAreaStatuses;
            Name = name;
        }
    }
}
