using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sensormatic.Tool.Ioc;

namespace Sensormatic.Tool.Sms
{
    public interface ISmsService : IScopedDependency
    {
        Task<SmsResponse> SendAsync(SmsRequest request);
    }
}
