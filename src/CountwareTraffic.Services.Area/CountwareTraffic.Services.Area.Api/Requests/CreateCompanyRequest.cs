using Mhd.Framework.Core;

namespace CountwareTraffic.Services.Areas.Api
{
    public class CreateCompanyRequest : RequestValidate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GsmNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FaxNumber { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ValidateResults.Add(new ErrorResult($"Value cannot be null", nameof(Name)));

            if (!string.IsNullOrEmpty(EmailAddress) && !EmailValidator.EmailIsValid(EmailAddress))
                ValidateResults.Add(new ErrorResult($"Email address invalid format", nameof(EmailAddress)));
        }
    }
}


