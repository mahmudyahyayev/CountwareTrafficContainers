using Mhd.Framework.Core;
using System;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [ServiceLog]
    public sealed partial class GetCitiesRequest : SensormaticRequestValidate
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

        public override void Validate()
        {
            if (_CountryId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"CountryId cannot be null", nameof(_CountryId)));
        }
    }
}
