using Mhd.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace Mhd.Framework.PushNotification
{
    public interface IPushNotificationService : IScopedDependency
    {
        Task<PushNotificationResponse> SendTargetedPush(PushNotificationRequest pushNotificationRequest);
    }

    public class PushNotificationService : IPushNotificationService
    {
        public PushNotificationService()
        {
            //todo:
        }

        public async Task<PushNotificationResponse> SendTargetedPush(PushNotificationRequest pushNotificationRequest)
        {
            return new PushNotificationResponse { IsSuccess = false, Message = "notimplemented" };
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
