using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public class DistrictAlreadyExistsException : DomainException
    {
        public string DistrictName { get; }

        public DistrictAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"District with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            DistrictName = name;
        }
    }
}
