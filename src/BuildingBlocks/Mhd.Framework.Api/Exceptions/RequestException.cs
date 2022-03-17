using Mhd.Framework.Core;
using System.Collections.Generic;

namespace Mhd.Framework.Api
{
    internal class RequestException : BaseException
    {
        internal RequestException(ICollection<ErrorResult> errorResults, int code, ResponseMessageType responseMessageType) 
            : base(errorResults, code, responseMessageType)
        {
        }
    }
}
