using System;
using System.Threading.Tasks;
using Sensormatic.Tool.Ioc;

namespace Sensormatic.Tool.Email
{
    public interface IEmailService : IScopedDependency
    {
        Task<EmailResponse> SendAsync(EmailRequest request);
    }
}
