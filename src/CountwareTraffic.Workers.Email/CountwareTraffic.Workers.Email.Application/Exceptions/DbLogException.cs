using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Workers.Email.Application
{
    public class DbLogException : AppException
    {
        public string Message { get; set; }
        public DbLogException(string message) : base(new List<ErrorResult>() { new ErrorResult(message) }, 500, ResponseMessageType.Error)
        {
            Message = message;
        }
    }
}
