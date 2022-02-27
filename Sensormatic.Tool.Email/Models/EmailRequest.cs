using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Sensormatic.Tool.Email
{
    public class EmailRequest
    {
        public IList<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<IFormFile> Attachments { get; set; }
        public bool IsHtml { get; set; }
    }
}
