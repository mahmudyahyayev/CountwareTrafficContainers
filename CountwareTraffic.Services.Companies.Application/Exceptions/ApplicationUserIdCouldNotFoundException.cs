using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Companies.Application
{
    public class ApplicationUserIdCouldNotFoundException : AppException
    {
        public ApplicationUserIdCouldNotFoundException()
            : base(new List<ErrorResult>() { new ErrorResult("ApplicationUserId could not found in settings") }, 404, ResponseMessageType.Error)
        {
            
        }
    }
}
