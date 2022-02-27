using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Services.Companies.Core
{
    public class SubAreaStatus : Enumeration
    {
        public static SubAreaStatus Created = new(1, nameof(Created));
        public static SubAreaStatus Completed = new(2, nameof(Completed));
        public static SubAreaStatus Rejected = new(3, nameof(Rejected));
       
        public SubAreaStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<SubAreaStatus> List() => new[] { Created, Completed, Rejected };

        public static SubAreaStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new SubAreaStatusNameNotFoundException(List(), name);

            return state;
        }

        public static SubAreaStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new SubAreaStatusIdNotFoundException(List(), id);

            return state;
        }
    }
}
