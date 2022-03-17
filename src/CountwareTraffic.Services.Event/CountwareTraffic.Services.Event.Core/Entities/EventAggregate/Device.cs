using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Events.Core
{
    public class Device : IEntity, IDeletable
    {
        public Guid _id;
        public string _name;

        public Guid Id => _id;
        public string Name => _name;

        public bool AuditIsDeleted { get; set ; }

        public static Device Create(Guid id, string name)
        {
            return new Device
            {
                _id = id,
                _name = name
            };
        }

        public void Change(string name)
            => _name = name;
    }
}
