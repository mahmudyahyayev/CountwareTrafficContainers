using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace Sensormatic.Tool.ElasticSearch
{
    public abstract class ElasticException : BaseException
    {
        public ElasticException(ICollection<ErrorResult> errorResults, int statusCode, ResponseMessageType responseMessageType)
            : base(errorResults, statusCode, responseMessageType) { }
    }
}
