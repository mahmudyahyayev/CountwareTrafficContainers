using Sensormatic.Tool.Core;
using System;

namespace CountwareTraffic.Services.Companies.Grpc
{
    [ServiceLog]
    public sealed partial class GetCityRequest : SensormaticRequestValidate
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
