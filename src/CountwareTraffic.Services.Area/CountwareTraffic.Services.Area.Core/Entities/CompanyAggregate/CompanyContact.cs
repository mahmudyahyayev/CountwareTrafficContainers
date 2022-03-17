namespace CountwareTraffic.Services.Areas.Core
{
    public record CompanyContact
    {
        private string _gsmNumber;
        private string _phoneNumber;
        private string _emailAddress;
        private string _faxNumber;

        public string GsmNumber => _gsmNumber;
        public string PhoneNumber => _phoneNumber;
        public string EmailAddress => _emailAddress;
        public string FaxNumber => _faxNumber;

        private CompanyContact() { }

        public static CompanyContact Create(string gsmNumber, string phoneNumber, string emailAddress, string faxNumber)
        {
            return new CompanyContact
            {
                _gsmNumber = gsmNumber,
                _phoneNumber = phoneNumber,
                _emailAddress = emailAddress,
                _faxNumber = faxNumber
            };
        }
    }
}
