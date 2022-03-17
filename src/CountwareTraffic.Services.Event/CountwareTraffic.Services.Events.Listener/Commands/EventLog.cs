using Convey.CQRS.Commands;
using CountwareTraffic.Services.Events.Application;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CountwareTraffic.Services.Events.Listener
{
    [Contract]
    public class EventLog : ICommand
    {
        public string Description { get; set; }
        [JsonIgnore]
        public Guid DeviceId { get; set; }
        public int DirectionTypeId { get; set; }
        public DateTime EventDate { get; set; }
        [JsonIgnore]
        public string DeviceName { get; set; }
    }
}
