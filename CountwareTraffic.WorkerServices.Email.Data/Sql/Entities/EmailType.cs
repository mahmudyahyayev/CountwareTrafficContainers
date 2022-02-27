using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.WorkerServices.Email.Data
{
    public class EmailType : Enumeration
    {
        public static EmailType Default = new(1, nameof(Default));
        public static EmailType Templated = new(2, nameof(Templated));

        public EmailType(int id, string name) : base(id, name) { }

        public static IEnumerable<EmailType> List() => new[] { Default, Templated };


        public static EmailType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return state;
        }

        public static EmailType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            return state;
        }
    }
}

