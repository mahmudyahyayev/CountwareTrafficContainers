using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Workers.SignalR.Application
{
    public class ApplicationUserIdCouldNotFoundException : AppException
    {
        public ApplicationUserIdCouldNotFoundException()
            : base(new List<ErrorResult>() { new ErrorResult("ApplicationUserId could not found in settings") }, 404, ResponseMessageType.Error)
        {
            
        }
    }
}
