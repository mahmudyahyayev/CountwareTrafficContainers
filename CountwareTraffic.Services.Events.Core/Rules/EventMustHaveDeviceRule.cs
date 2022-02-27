using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Events.Core
{
    public class EventMustHaveDeviceRule : IBusinessRule
    {
        private readonly Guid _deviceId;

        public EventMustHaveDeviceRule(Guid deviceId)
            => _deviceId = deviceId;

        public bool IsBroken() => _deviceId == Guid.Empty;
        public string Message => "Event must have device";
    }
}
