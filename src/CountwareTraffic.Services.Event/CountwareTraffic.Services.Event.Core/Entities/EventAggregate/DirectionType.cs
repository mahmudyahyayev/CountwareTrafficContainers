using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Events.Core
{
    public class DirectionType : Enumeration
    {
        public static DirectionType Unknown = new(1, nameof(Unknown));
        public static DirectionType Inwards = new(2, nameof(Inwards));
        public static DirectionType Outward = new(3, nameof(Outward));

        public DirectionType(int id, string name) : base(id, name) { }

        public static IEnumerable<DirectionType> List() => new[] { Unknown, Inwards, Outward };


        public static DirectionType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new DirectionTypeNameNotFoundException(List(), name);

            return state;
        }

        public static DirectionType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new DirectionTypeIdNotFoundException(List(), id);

            return state;
        }
    }
}
