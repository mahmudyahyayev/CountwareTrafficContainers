using Sensormatic.Tool.Efcore;
using System;

namespace CountwareTraffic.WorkerServices.Sms.Infrastructure
{
    public class Sms : IEntity
    {
        public Guid _id;
        private string _userIds;
        private string _phoneNumbers;
        private string _request;
        private string _response;
        private bool _isSuccess;

        public Guid Id => _id;
        public string UserIds => _userIds;
        public string PhoneNumbers => _phoneNumbers;
        public string Request => _request;
        public string Response => _response;
        public bool IsSuccess => _isSuccess;
    }
}
