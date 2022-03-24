using Mhd.Framework.Core;
using System;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [ServiceLog]
    public sealed partial class UpdateCountryRequest : RequestValidate
    {
        internal Guid _CountryId
        {
            get
            {
                if (Guid.TryParse(countryId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { this.countryId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_CountryId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"CountryId cannot be null", nameof(_CountryId)));

            if (String.IsNullOrEmpty(this.Name))
                ValidateResults.Add(new ErrorResult($"Name Value cannot be null", nameof(Name)));
        }
    }
}
