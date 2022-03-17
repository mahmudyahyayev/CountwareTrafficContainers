namespace CountwareTraffic.Services.Areas.Core
{
    public record AreaContact
    {
        private string _gsmNumber;
        private string _phoneNumber;
        private string _phoneNumber2;
        private string _emailAddress;
        private string _faxNumber;

        public string GsmNumber => _gsmNumber;
        public string PhoneNumber => _phoneNumber;
        public string PhoneNumber2 => _phoneNumber2;
        public string EmailAddress => _emailAddress;
        public string FaxNumber => _faxNumber;

        private AreaContact() { }

        public static AreaContact Create(string gsmNumber, string phoneNumber, string phoneNumber2, string emailAddress, string faxNumber)
        {
            return new AreaContact
            {
                _gsmNumber = gsmNumber,
                _phoneNumber = phoneNumber,
                _phoneNumber2 = phoneNumber2,
                _emailAddress = emailAddress,
                _faxNumber = faxNumber
            };
        }
    }
}
