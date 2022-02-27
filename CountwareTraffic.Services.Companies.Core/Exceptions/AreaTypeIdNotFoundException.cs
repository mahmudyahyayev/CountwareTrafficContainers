using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Companies.Core
{
    public class AreaTypeIdNotFoundException : DomainException
    {
        IEnumerable<AreaType> AreaTypes { get; }
        public int Id { get; }

        public AreaTypeIdNotFoundException(IEnumerable<AreaType> areaTypes, int id)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for AreaType Id: {String.Join(",", areaTypes.Select(s => s.Id))}") }, 400, ResponseMessageType.Error)
        {
            AreaTypes = areaTypes;
            Id = id;
        }
    }
}
