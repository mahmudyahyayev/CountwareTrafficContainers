using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Core
{
    public class SubAreaStatusIdNotFoundException : DomainException
    {
        IEnumerable<SubAreaStatus> SubAreaStatuses { get; }
        public int Id { get; }

        public SubAreaStatusIdNotFoundException(IEnumerable<SubAreaStatus> subAreaStatuses, int id)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for SubAreaStatus Id: {String.Join(",", subAreaStatuses.Select(s => s.Id))}") }, 400, ResponseMessageType.Error)
        {
            SubAreaStatuses = subAreaStatuses;
            Id = id;
        }
    }
}
