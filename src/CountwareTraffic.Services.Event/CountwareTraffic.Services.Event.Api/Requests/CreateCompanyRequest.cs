using Mhd.Framework.Core;

namespace CountwareTraffic.Services.Events.Api
{
    public class CreateEventRequest : SensormaticRequestValidate
    {
        public string Description { get; set; }
        public int DirectionTypeId { get; set; }

        public override void Validate() { }
    }
}


