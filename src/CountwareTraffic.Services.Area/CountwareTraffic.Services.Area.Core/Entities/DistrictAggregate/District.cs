using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Areas.Core
{
    public class District : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _name;
        private Guid _cityId;

        public string Name => _name;
        public Guid CityId => _cityId;

        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public bool AuditIsDeleted { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        #endregion default properties

        private District() { }

        public static District Create(string name, Guid cityId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return new District
            {
                _name = name,
                _cityId = cityId
            };
        }

        public void CompleteChange(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (_name != name) _name = name;
        }
    }
}
