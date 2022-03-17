using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace Mhd.Framework.Email
{
    public interface IEmailService : IScopedDependency
    {
        Task<EmailResponse> SendAsync(EmailRequest request);
    }

    public class EmailService : IEmailService
    {
        public EmailService()
        {
            //todo
        }

        public async Task<EmailResponse> SendAsync(EmailRequest request)
        {
            //todo send mail code

            return new EmailResponse { IsSuccess = false, Message = "notimplemented" };
        }


        public void Dispose() => GC.SuppressFinalize(this);
    }
}
