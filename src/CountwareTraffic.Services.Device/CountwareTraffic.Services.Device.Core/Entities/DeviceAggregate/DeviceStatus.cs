using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceStatus : Enumeration
    {
        public static DeviceStatus Unknown = new(1, nameof(Unknown));
        public static DeviceStatus Connected = new(2, nameof(Connected));
        public static DeviceStatus DisConnected = new(3, nameof(DisConnected));
        public static DeviceStatus Broken = new(4, nameof(Broken));

        public DeviceStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<DeviceStatus> List() => new[] { Unknown, Connected, DisConnected, Broken };

        public static DeviceStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new DeviceStatusNameNotFoundException(List(), name);

            return state;
        }

        public static DeviceStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new DeviceStatusIdNotFoundException(List(), id);

            return state;
        }

        public static bool TryParse(int id, out DeviceStatus deviceStatus)
        {
            deviceStatus = List().SingleOrDefault(s => s.Id == id);

            if (deviceStatus == null)
                return false;

            return true;
        }
    }
}
