using Nest;
using System;

namespace Sensormatic.Tool.ElasticSearch
{
    [ElasticsearchType(IdProperty = "Id")]
    public abstract class ElasticSearchEntity
    {
        public Guid Id { get; set; }
    }
}
