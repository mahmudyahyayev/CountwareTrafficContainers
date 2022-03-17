using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public class CityAlreadyExistsException : DomainException
    {
        public string CityName { get; }

        public CityAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"City with name: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            CityName = name;
        }
    }
}
