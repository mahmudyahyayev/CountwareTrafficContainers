using Mhd.Framework.Core;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator
{
    public class AddCountryRequest : RequestValidate
    {
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public int IsoNumeric { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string ContinentCode { get; set; }
        public string CurrencyCode { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));
        }
    }

}
