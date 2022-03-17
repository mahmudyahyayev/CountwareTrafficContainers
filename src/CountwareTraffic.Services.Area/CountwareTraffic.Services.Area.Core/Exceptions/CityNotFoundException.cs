using Mhd.Framework.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public class CityNotFoundException : DomainException
    {
        public Guid CityId { get; }

        public CityNotFoundException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"City with id: {id} not found.") }, 404, ResponseMessageType.Error)
        {
            CityId = id;
        }
    }
}
