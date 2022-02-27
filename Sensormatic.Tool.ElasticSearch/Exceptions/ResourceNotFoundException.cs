using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace Sensormatic.Tool.ElasticSearch
{
    public class ResourceNotFoundException : ElasticException
    {
        public string Index { get; set; }
        public string Id { get; }

        public ResourceNotFoundException(string index, string id)
            : base(new List<ErrorResult>() { new ErrorResult($"{index} data with id: {id} not found.") }, 404, ResponseMessageType.Error)
        {
            Index = index;
            Id = id;
        }
    }
}
