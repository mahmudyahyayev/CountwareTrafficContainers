using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public class SubAreaAlreadyExistsException : DomainException
    {
        public string SubAreaName { get; }

        public SubAreaAlreadyExistsException(string name)
            : base(new List<ErrorResult>() { new ErrorResult($"SubArea with id: {name} already exists.") }, 409, ResponseMessageType.Error)
        {
            SubAreaName = name;
        }
    }
}

