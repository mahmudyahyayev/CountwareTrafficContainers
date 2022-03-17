using Convey.CQRS.Commands;
using System;

namespace CountwareTraffic.Services.Events.Application
{
    [Contract]
    public class CreateEvent : ICommand
    {
        public Guid DeviceId { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int DirectionTypeId { get; set; }
    }
}
