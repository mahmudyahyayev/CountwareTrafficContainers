using Mhd.Framework.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public abstract class DomainException : BaseException
    {
        public DomainException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType)
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
