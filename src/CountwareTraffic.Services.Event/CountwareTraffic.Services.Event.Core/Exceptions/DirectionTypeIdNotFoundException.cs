using CountwareTraffic.Services.Events.Core;
using Mhd.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Events.Core
{
    public class DirectionTypeIdNotFoundException : DomainException
    {
        IEnumerable<DirectionType> DirectionTypes { get; }
        public int Id { get; }

        public DirectionTypeIdNotFoundException(IEnumerable<DirectionType> directionTypes, int id)
            : base(new List<ErrorResult>() { new ErrorResult($"Possible values for DirectionType Id: {String.Join(",", directionTypes.Select(s => s.Id))}") }, 400, ResponseMessageType.Error)
        {
            DirectionTypes = directionTypes;
            Id = id;
        }
    }
}
