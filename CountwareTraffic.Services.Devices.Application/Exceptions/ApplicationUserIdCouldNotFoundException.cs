using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Devices.Application
{
    public class ApplicationUserIdCouldNotFoundException : AppException
    {
        public ApplicationUserIdCouldNotFoundException()
            : base(new List<ErrorResult>() { new ErrorResult("ApplicationUserId could not found in settings") }, 409, ResponseMessageType.Error)
        {
            
        }
    }
}
