using Sensormatic.Tool.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Users.Application
{
    public class GetTokenException : AppException
    {
        public string UserName { get; }

        public GetTokenException(string userName)
            : base(new List<ErrorResult>() { new ErrorResult($"User with name: {userName} informations is incorrect. Please check and try again.") }, 404, ResponseMessageType.Error)
        {
            UserName = userName;
        }
    }
}
