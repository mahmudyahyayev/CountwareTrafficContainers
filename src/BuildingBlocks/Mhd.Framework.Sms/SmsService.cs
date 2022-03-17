using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace Mhd.Framework.Sms
{
    public interface ISmsService : IScopedDependency
    {
        Task<SmsResponse> SendAsync(SmsRequest request);
    }

    public class SmsService : ISmsService
    {
        public SmsService()
        {
            //todo
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
