using Mhd.Framework.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public class DistrictNotFoundException : DomainException
    {
        public Guid DistrictId { get; }

        public DistrictNotFoundException(Guid id)
            : base(new List<ErrorResult>() { new ErrorResult($"District with id: {id} not found.") }, 404, ResponseMessageType.Error)
        {
            DistrictId = id;
        }
    }
}
