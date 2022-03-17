using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Areas.Core
{
    public class SubArea : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _name;
        private string _description;
        private Guid _areaId;
        private int _subAreaStatus;

        public string Name => _name;
        public string Description => _description;
        public Guid AreaId => _areaId;
        public SubAreaStatus SubAreaStatus { get; private set; }


        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        public bool AuditIsDeleted { get; set; }
        #endregion default properties

        private SubArea() { }

        public static SubArea Create(string name, string description, Guid areaId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            SubArea subArea = new()
            {
                _name = name,
                _description = description,
                _areaId = areaId,
                _subAreaStatus = SubAreaStatus.Created.Id
            };

            return subArea;
        }

        public void WhenCreated(Guid subAreaId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (subAreaId == Guid.Empty)
                throw new ArgumentNullException(nameof(subAreaId));

            AddEvent(new SubAreaCreated(subAreaId, name));
        }

        public void WhenChanged(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (name != _name)
                AddEvent(new SubAreaChanged(_id, name, _name));

            _name = name;
            _description = description;
        }

        public void WhenDeleted(Guid subAreaId)
        {
            if (subAreaId == Guid.Empty)
                throw new ArgumentNullException(nameof(subAreaId));

            AddEvent(new SubAreaDeleted(subAreaId));
        }

        public void WhenCreatedCompleted() => _subAreaStatus = SubAreaStatus.Completed.Id;

        public void WhenCreatedRejected() => _subAreaStatus = SubAreaStatus.Rejected.Id;

        public void WhenChangedRejected(string oldName) => _name = oldName;

        public void WhenDeletedRejected() => AuditIsDeleted = false;
    }
}
