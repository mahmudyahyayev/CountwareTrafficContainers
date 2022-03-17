using Mhd.Framework.Core;
using System;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [ServiceLog]
    public sealed partial class DeleteSubAreaRequest : SensormaticRequestValidate
    {
        internal Guid _SubAreaId
        {
            get
            {
                if (Guid.TryParse(subAreaId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { this.subAreaId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_SubAreaId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"SubAreaId cannot be null", nameof(_SubAreaId)));
        }
    }
}
