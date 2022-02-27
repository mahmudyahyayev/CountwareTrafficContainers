using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.Services.Companies.Core
{
    public class Country : AggregateRoot, IAuditable, IDateable, IDeletable, ITraceable
    {
        private string _iso;
        private string _iso3;
        private int _isoNumeric;
        private string _name;
        private string _capital;
        private string _continentCode;
        private string _currencyCode;
        private Guid _companyId;

        public string Iso => _iso;
        public string Iso3 => _iso3;
        public int IsoNumeric => _isoNumeric;
        public string Name => _name;
        public string Capital => _capital;
        public string ContinentCode => _continentCode;
        public string CurrencyCode => _currencyCode;
        public Guid CompanyId => _companyId;


        #region default properties
        public DateTime AuditCreateDate { get; set; }
        public DateTime AuditModifiedDate { get; set; }
        public bool AuditIsDeleted { get; set; }
        public Guid AuditCreateBy { get; set; }
        public Guid AuditModifiedBy { get; set; }
        #endregion default properties

        private Country() { }

        public static Country Create(string iso, string iso3, int isoNumeric, string name, string capital, string continentCode, string currencyCode, Guid companyId)
        {
            if (string.IsNullOrEmpty(iso))
                throw new ArgumentNullException(nameof(iso));

            if (iso.Length != 2)
                throw new InvalidIsoCodeException(iso);


            if (string.IsNullOrEmpty(iso3) && iso.Length != 3)
                throw new InvalidIsoCodeException(iso3);


            if (isoNumeric < 1)
                throw new ArgumentException("IsoNumeric must be above zero.", nameof(isoNumeric));


            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));


            if (string.IsNullOrEmpty(capital))
                throw new ArgumentNullException(nameof(capital));


            if (!string.IsNullOrEmpty(continentCode) && continentCode.Length != 2)
                throw new InvalidContinentCodeException(continentCode);


            if (!string.IsNullOrEmpty(currencyCode) && currencyCode.Length != 3)
                throw new InvalidCurrencyCodeException(currencyCode);

            return new Country
            {
                _iso = iso,
                _iso3 = iso3,
                _isoNumeric = isoNumeric,
                _name = name,
                _capital = capital,
                _continentCode = continentCode,
                _currencyCode = currencyCode,
                _companyId = companyId
            };
        }


        public void CompleteChange(string iso, string iso3, int isoNumeric, string name, string capital, string continentCode, string currencyCode)
        {
            if (string.IsNullOrEmpty(iso))
                throw new ArgumentNullException(nameof(iso));

            if (iso.Length != 2)
                throw new InvalidIsoCodeException(iso);


            if (string.IsNullOrEmpty(iso3) && iso.Length != 3)
                throw new InvalidIsoCodeException(iso3);


            if (isoNumeric < 1)
                throw new ArgumentException("IsoNumeric must be above zero.", nameof(isoNumeric));


            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));


            if (string.IsNullOrEmpty(capital))
                throw new ArgumentNullException(nameof(capital));


            if (string.IsNullOrEmpty(continentCode) && iso.Length != 2)
                throw new InvalidContinentCodeException(continentCode);


            if (string.IsNullOrEmpty(currencyCode) && iso.Length != 3)
                throw new InvalidCurrencyCodeException(currencyCode);

            _iso = iso;
            _iso3 = iso3;
            _isoNumeric = isoNumeric;
            _name = name;
            _capital = capital;
            _continentCode = continentCode;
            _currencyCode = currencyCode;
        }
    }
}

