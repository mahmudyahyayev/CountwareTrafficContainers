using Sensormatic.Tool.Ioc;
using System;
using System.Threading.Tasks;

namespace Sensormatic.Tool.PushNotification
{
    public interface IPushNotificationService : IScopedDependency
    {
        Task<PushNotificationResponse> SendTargetedPush(PushNotificationRequest pushNotificationRequest);
    }
}
