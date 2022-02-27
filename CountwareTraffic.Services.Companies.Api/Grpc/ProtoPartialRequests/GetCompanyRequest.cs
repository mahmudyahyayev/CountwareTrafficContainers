﻿using Sensormatic.Tool.Core;
using System;

namespace CountwareTraffic.Services.Companies.Grpc
{
    [ServiceLog]
    public sealed partial class GetCompanyRequest : SensormaticRequestValidate
    {
        internal Guid _CompanyId
        {
            get
            {
                if (Guid.TryParse(this.companyId_, out Guid id))
                    return id;

                return Guid.Empty;
            }
            set { this.companyId_ = value.ToString(); }
        }

        public override void Validate()
        {
            if (_CompanyId == Guid.Empty)
                ValidateResults.Add(new ErrorResult($"SubAreaId cannot be null", nameof(_CompanyId)));
        }
    }
}
