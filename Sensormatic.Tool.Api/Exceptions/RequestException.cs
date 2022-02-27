using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace Sensormatic.Tool.Api
{
    internal class RequestException : BaseException
    {
        internal RequestException(ICollection<ErrorResult> errorResults, int code, ResponseMessageType responseMessageType) 
            : base(errorResults, code, responseMessageType)
        {
        }
    }
}
