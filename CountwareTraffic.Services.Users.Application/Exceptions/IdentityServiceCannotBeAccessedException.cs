using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Users.Application
{
    public class IdentityServiceCannotBeAccessedException : AppException
    {
        public IdentityServiceCannotBeAccessedException()
            : base(new List<ErrorResult>() { new ErrorResult($"Identity Service cannot be accessed or there is a system error.") }, 500, ResponseMessageType.Error) { }
    }
}
