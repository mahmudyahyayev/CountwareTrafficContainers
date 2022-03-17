using Mhd.Framework.Efcore;
using System;

namespace CountwareTraffic.Workers.Email.Data
{
    public class EmailLog : IEntity
    {
        public Guid _id;
        private string _userIds;
        private string _emailSubject;
        private string _emailBody;
        private string _emailTo;
        private string _response;
        private int _emailTypeId;
        private bool _isHtml;


        public Guid Id => _id;
        public string UserIds => _userIds;
        public string EmailSubject => _emailSubject;
        public string EmailBody => _emailBody;
        public string EmailTo => _emailTo;
        public string Response => _response;
        public bool IsHtml => _isHtml;
        public EmailType EmailType { get; private set; }

        public static EmailLog Create(string userIds, string emailSubject, string emailBody, string emailTo, bool isHtml, string response, int emailTypeId)
        {
            return new EmailLog
            {
                _userIds = userIds,
                _emailSubject = emailSubject,
                _emailBody = emailBody,
                _emailTo = emailTo,
                _response = response,
                _isHtml = isHtml,
                _emailTypeId = emailTypeId
            };
        }
    }
}
