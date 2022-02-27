using Sensormatic.Tool.Core;
using System;

namespace CountwareTraffic.Services.Devices.Grpc
{
    [ServiceLog]
    public sealed partial class GetDevicesRequest : SensormaticRequestValidate
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


        internal PagingRequest _PagingRequest
        {
            get
            {
                if (this.pagingRequest_ == null)
                {
                    this.pagingRequest_ = new PagingRequest() { Limit = 10, Page = 1 };
                    return this.pagingRequest_;
                }

                if (this.pagingRequest_.Limit < 1)
                    this.pagingRequest_.Limit = 10;

                if (this.pagingRequest_.Page < 1)
                    this.pagingRequest_.Page = 1;

                return pagingRequest_;
            }
            set { this.pagingRequest_ = value; }
        }

        public override void Validate() { }
    }
}

