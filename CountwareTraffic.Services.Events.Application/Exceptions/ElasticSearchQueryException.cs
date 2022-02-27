using Sensormatic.Tool.Core;
using System;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public class ElasticSearchQueryException : AppException
    {
        public string IndexName { get; }
        public int HttpStatusCode { get; }
        public string Type { get; }

        public ElasticSearchQueryException(string indexName, int httpStatusCode, string type, Exception exception) : base(new List<ErrorResult>() { new ErrorResult(exception.Message) }, httpStatusCode, ResponseMessageType.Error)
        {
            IndexName = indexName;
            HttpStatusCode = httpStatusCode;
            Type = type;
        }
    }
}
