using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Devices.Core
{
    public class SubArea : IEntity, IDeletable
    {
        public string _name;
        public Guid _id;

        public string Name => _name;
        public Guid Id => _id;

        public bool AuditIsDeleted { get; set; }
        private SubArea() { }
        public static SubArea Create(Guid id, string name)
        {
            return new SubArea
            {
                _id = id,
                _name = name
            };
        }

        public void Change(string name)
            => _name = name;
    }
}
