using System;
using System.Threading.Tasks;

namespace Sensormatic.Tool.PushNotification
{
    public class PushNotificationService : IPushNotificationService
    {
        public PushNotificationService()
        {

        }

        public async Task<PushNotificationResponse> SendTargetedPush(PushNotificationRequest pushNotificationRequest)
        {
            return new PushNotificationResponse { IsSuccess = false, Message = "notimplemented" };
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
