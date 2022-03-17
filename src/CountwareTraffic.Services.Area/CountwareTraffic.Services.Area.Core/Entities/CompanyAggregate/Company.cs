using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Services.Areas.Core
{
    public class Company : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _name;
        private string _description;
        private CompanyAddress _address;
        private CompanyContact _contact;

        public string Name => _name;
        public string Description => _description;

        public CompanyAddress Address => _address;
        public CompanyContact Contact => _contact;


        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public bool AuditIsDeleted { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        #endregion default properties

        private Company() { }


        public static Company Create(string name, string description, CompanyAddress address, CompanyContact contact)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return new Company
            {
                _name = name,
                _description = description,
                _address = address,
                _contact = contact
            };
        }

        public void CompleteChange(string name, string description, CompanyAddress address, CompanyContact contact)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (name != _name) _name = name;

            if (description != _description) _description = description;

            if (address != _address) _address = address;

            if (contact != _contact) _contact = contact;
        }
    }
}
