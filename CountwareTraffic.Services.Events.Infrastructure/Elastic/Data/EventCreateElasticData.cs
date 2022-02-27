using Sensormatic.Tool.ElasticSearch;
using System;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventCreateElasticData : ElasticSearchEntity
    {
        public EventCreateElasticData() 
            => Id = Guid.NewGuid();

        public Guid DeviceId { get; set; }
        public Guid EventId { get; set; }
        public string DeviceName { get; set; }
        public int DirectionTypeId { get; set; }
        public string DirectionTypeName { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
