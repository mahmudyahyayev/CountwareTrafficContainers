using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Companies.Core
{
    public class City : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _name;
        private Guid _countryId;

        public string Name => _name;
        public Guid CountryId => _countryId;


        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public bool AuditIsDeleted { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        #endregion default properties

        private City() { }

        public static City Create(string name, Guid countryId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return new City
            {
                _name = name,
                _countryId = countryId
            };
        }

        public void CompleteChange(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            _name = name;
        }
    }
}
