using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceType : Enumeration
    {
        public static DeviceType Unknown = new(1, nameof(Unknown));
        public static DeviceType Bio = new(2, nameof(Bio));
        public static DeviceType AccessControl = new(3, nameof(AccessControl));

        public DeviceType(int id, string name) : base(id, name) { }

        public static IEnumerable<DeviceType> List() => new[] { Unknown, Bio, AccessControl };


        public static DeviceType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new DeviceTypeNameNotFoundException(List(), name);

            return state;
        }

        public static DeviceType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new DeviceTypeIdNotFoundException(List(), id);

            return state;
        }

        public static bool TryParse(int id, out DeviceType deviceType)
        {
            deviceType = List().SingleOrDefault(s => s.Id == id);

            if (deviceType == null)
                return false;

            return true;
        }
    }
}
