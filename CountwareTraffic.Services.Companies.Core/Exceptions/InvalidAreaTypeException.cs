using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Core
{
    public class InvalidAreaTypeException : DomainException
    {
        public int AreaTypeId { get; }

        public InvalidAreaTypeException(int areaTypeId)
            : base(new List<ErrorResult>() { new ErrorResult($"AreaTypeId with id: {areaTypeId} invalid format.") }, 400, ResponseMessageType.Error)
        {
            AreaTypeId = areaTypeId;
        }
    }
}