using Mhd.Framework.Core;
using System;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [ServiceLog]
    public sealed partial class CreateDistrictRequest : SensormaticRequestValidate
    {
        internal Guid _CityId
        {
            get
            {
                if (Guid.TryParse(cityId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { this.cityId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_CityId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"CityId cannot be null", nameof(_CityId)));

            if (String.IsNullOrEmpty(this.Name))
                ValidateResults.Add(new ErrorResult($"Name Value cannot be null", nameof(Name)));
        }
    }
}
