using System;
using System.Collections.Generic;
using System.Linq;

namespace CountwareTraffic.Workers.Sms.Data
{
    public class SmsType : Enumeration
    {
        public static SmsType Default = new(1, nameof(Default));
        public static SmsType Templated = new(2, nameof(Templated));

        public SmsType(int id, string name) : base(id, name) { }

        public static IEnumerable<SmsType> List() => new[] { Default, Templated };


        public static SmsType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return state;
        }

        public static SmsType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            return state;
        }
    }
}

