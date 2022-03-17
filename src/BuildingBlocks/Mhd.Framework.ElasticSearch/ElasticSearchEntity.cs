using Nest;
using System;

namespace Mhd.Framework.ElasticSearch
{
    [ElasticsearchType(IdProperty = "Id")]
    public abstract class ElasticSearchEntity
    {
        public Guid Id { get; set; }
    }
}
