using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Areas.Core
{
    public class AreaType : Enumeration
    {
        public static AreaType Unknown = new(1, nameof(Unknown));
        public static AreaType Franchising = new(2, nameof(Franchising));
        public static AreaType CompanyOwned = new(3, nameof(CompanyOwned));

        public AreaType(int id, string name) : base(id, name) { }

        public static IEnumerable<AreaType> List() => new[] { Unknown, Franchising, CompanyOwned };


        public static AreaType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new AreaTypeNameNotFoundException(List(), name);

            return state;
        }

        public static AreaType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new AreaTypeIdNotFoundException(List(), id);

            return state;
        }
    }
}
