using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Email
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public async Task<EmailResponse> SendAsync(EmailRequest request)
        {
            //todo send mail code

            return new EmailResponse { IsSuccess = false, Message = "notimplemented" };
        }


        public void Dispose() 
            => GC.SuppressFinalize(this);
      
    }
}
