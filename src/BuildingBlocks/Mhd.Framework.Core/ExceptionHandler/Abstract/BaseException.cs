using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Mhd.Framework.Core
{
    public abstract class BaseException : Exception
    {
        public ErrorModel ErrorModel { get; private set; }

       
        public BaseException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType) : base(string.Join(Environment.NewLine, errorResults.Select(x => x.ToString())))
        {
            ErrorModel = new ErrorModel(errorResults, statusCode, responseMessageType);
        }
    }
}
