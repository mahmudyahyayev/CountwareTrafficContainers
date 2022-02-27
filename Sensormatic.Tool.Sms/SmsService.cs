using System;
using System.Threading.Tasks;

namespace Sensormatic.Tool.Sms
{
    public class SmsService : ISmsService
    {
        public SmsService()
        {

        }
        public async Task<SmsResponse> SendAsync(SmsRequest request)
        {
            //todo send mail code

            return new SmsResponse { IsSuccess = false, Message = "notimplemented" };
        }

        public void Dispose()
            => GC.SuppressFinalize(this);
        
    }
}
