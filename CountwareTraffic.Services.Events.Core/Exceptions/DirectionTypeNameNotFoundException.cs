using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Events.Core
{
    public class DirectionTypeNameNotFoundException : DomainException
    {
        IEnumerable<DirectionType> DirectionTypes { get; }
        public string Name { get; }

        public DirectionTypeNameNotFoundException(IEnumerable<DirectionType> directionTypes, string name)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DirectionType Name: {String.Join(",", directionTypes.Select(s => s.Name))}") }, 400, ResponseMessageType.Error)
        {
            DirectionTypes = directionTypes;
            Name = name;
        }
    }
}
