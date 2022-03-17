using Mhd.Framework.Core;
using System.Collections.Generic;

namespace Mhd.Framework.ElasticSearch
{
    public abstract class ElasticException : BaseException
    {
        public ElasticException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType)
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
