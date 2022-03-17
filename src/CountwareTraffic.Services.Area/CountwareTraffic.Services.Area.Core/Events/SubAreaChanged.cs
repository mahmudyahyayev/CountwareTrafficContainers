using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Areas.Core
{
    public class SubAreaChanged : IDomainEvent
    {
        public Guid SubAreaId { get; init; }
        public string Name { get; init; }
        public string OldName { get; set; }
        public Guid RecordId { get; init; }

        public SubAreaChanged(Guid subAreaId, string name, string oldName)
        {
            SubAreaId = subAreaId;
            Name = name;
            OldName = oldName;
            RecordId = Guid.NewGuid();
        }
    }
}
