using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Devices.Core
{
    public class DeviceCreationStatus : Enumeration
    {
        public static DeviceCreationStatus Created = new(1, nameof(Created));
        public static DeviceCreationStatus Completed = new(2, nameof(Completed));
        public static DeviceCreationStatus Rejected = new(3, nameof(Rejected));

        public DeviceCreationStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<DeviceCreationStatus> List() => new[] { Created, Completed, Rejected };

        public static DeviceCreationStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new DeviceCreationStatusNameNotFoundException(List(), name);

            return state;
        }

        public static DeviceCreationStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new DeviceCreationStatusIdNotFoundException(List(), id);

            return state;
        }
    }
}
