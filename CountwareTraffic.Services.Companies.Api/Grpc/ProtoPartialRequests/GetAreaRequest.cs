using Sensormatic.Tool.Core;
using System;

namespace CountwareTraffic.Services.Companies.Grpc
{
    [ServiceLog]
    public sealed partial class GetAreaRequest : SensormaticRequestValidate
    {
        internal Guid _AreaId
        {
            get
            {
                if (Guid.TryParse(areaId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { this.areaId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_AreaId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"AreaId cannot be null", nameof(_AreaId)));
        }
    }
}
