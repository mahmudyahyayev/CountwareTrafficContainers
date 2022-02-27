using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Companies.Core
{
    public class Area : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _name;
        private string _description;
        private int _areaTypeId;
        private Guid _districtId;
        private AreaAddress _address;
        private AreaContact _contact;


        public string Name => _name;
        public string Description => _description;
        public AreaType AreaType { get; private set; }
        public Guid DistrictId => _districtId;
        public AreaAddress Address => _address;
        public AreaContact Contact => _contact;


        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public bool AuditIsDeleted { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        #endregion default properties

        private Area() { }

        public static Area Create(string name, string description, int areaTypeId, Guid districtId, AreaAddress address, AreaContact contact)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (areaTypeId < 1)
                throw new InvalidAreaTypeException(areaTypeId);

            var area = new Area
            {
                _name = name,
                _areaTypeId = areaTypeId,
                _description = description,
                _districtId = districtId,
                _address = address,
                _contact = contact
            };

            return area;
        }

        public void CompleteChange(string name, string description, int areaTypeId, AreaAddress address, AreaContact contact)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (areaTypeId < 1)
                throw new InvalidAreaTypeException(areaTypeId);

            if (areaTypeId != _areaTypeId)
            {
                ChangeAreaType(areaTypeId);
                _areaTypeId = areaTypeId;
            }

            if (_name != name) _name = name;

            if (_description != description) _description = description;

            if (_address != address) _address = address;

            if (_contact != contact) _contact = contact;
        }

        public void ChangeAreaType(int areaTypeId)
        {
            if (areaTypeId < 1)
                throw new InvalidAreaTypeException(areaTypeId);

            AddEvent(new AreaTypeChanged(this, areaTypeId));
        }
    }
}
