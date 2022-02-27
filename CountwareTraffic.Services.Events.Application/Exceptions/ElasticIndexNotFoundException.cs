using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Events.Application
{
    public class ElasticIndexNotFoundException : AppException
    {
        public string IndexName { get; }
        public ElasticIndexNotFoundException(string indexName) : base(new List<ErrorResult>() { new ErrorResult($"Elastic index with name: {indexName} not found.") }, 400, ResponseMessageType.Error)
        {
            IndexName = indexName;
        }
    }
}
