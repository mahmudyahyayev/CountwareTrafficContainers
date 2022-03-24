using Mhd.Framework.Core;
using System;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [ServiceLog]
    public sealed partial class DeleteCityRequest : RequestValidate
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
        }
    }
}
