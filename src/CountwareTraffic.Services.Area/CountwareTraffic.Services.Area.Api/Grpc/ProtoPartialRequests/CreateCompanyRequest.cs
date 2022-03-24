using CountwareTraffic.Services.Areas.Api;
using Mhd.Framework.Core;
using System;

namespace CountwareTraffic.Services.Areas.Grpc
{
    [ServiceLog]
    public sealed partial class CreateCompanyRequest : RequestValidate
    {
        public override void Validate()
        {
            if (String.IsNullOrEmpty(this.Name))
                ValidateResults.Add(new ErrorResult($"Name Value cannot be null", nameof(Name)));

            if (!string.IsNullOrEmpty(EmailAddress) && !EmailValidator.EmailIsValid(EmailAddress))
                ValidateResults.Add(new ErrorResult($"Email address invalid format", nameof(EmailAddress)));
        }
    }
}
