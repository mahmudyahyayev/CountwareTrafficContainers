using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.WorkerServices.Sms.Data
{
    public class SmsLog : IEntity
    {
        public Guid _id;
        private string _userIds;
        private string _phoneNumbers;
        private string _smsBody;
        private string _response;
        private bool _isOtp;
        private int _smsTypeId;
        public Guid Id => _id;
        public string UserIds => _userIds;
        public string PhoneNumbers => _phoneNumbers;
        public string SmsBody => _smsBody;
        public string Response => _response;
        public bool IsOtp => _isOtp;
        public SmsType SmsType { get; private set; }

        public static SmsLog Create(string userIds, string phoneNumbers, string smsBody, string response, bool isOtp, int smsTypeId )
        {
            return new SmsLog
            {
                _userIds = userIds,
                _isOtp = isOtp,
                _smsBody = smsBody,
                _phoneNumbers = phoneNumbers,
                _response = response,
                _smsTypeId = smsTypeId
            };
        }
    }
}
