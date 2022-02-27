namespace Sensormatic.Tool.Common
{
    public static class Queues
    {
        #region CountwareTraffic
        //Company
        public const string CountwareTrafficCompaniesSubAreaCreated = "Countware.Traffic.Companies.SubAreaCreated";
        public const string CountwareTrafficCompaniesSubAreaChanged = "Countware.Traffic.Companies.SubAreaChanged";
        public const string CountwareTrafficCompaniesSubAreaDeleted = "Countware.Traffic.Companies.SubAreaDeleted";
        public const string CountwareTrafficCompaniesSubAreaCreatedCompleted = "Countware.Traffic.Companies.SubAreaCreatedCompleted";
        public const string CountwareTrafficCompaniesSubAreaCreatedRejected = "Countware.Traffic.Companies.SubAreaCreatedRejected";
        public const string CountwareTrafficCompaniesSubAreaChangedRejected = "Countware.Traffic.Companies.SubAreaChangedRejected";
        public const string CountwareTrafficCompaniesSubAreaDeletedRejected = "Countware.Traffic.Companies.SubAreaDeletedRejected";

        //Device
        public const string CountwareTrafficDevicesDeviceCreated = "Countware.Traffic.Devices.DeviceCreated";
        public const string CountwareTrafficDevicesDeviceChanged = "Countware.Traffic.Devices.DeviceChanged";
        public const string CountwareTrafficDevicesDeviceDeleted = "Countware.Traffic.Devices.DeviceDeleted";
        public const string CountwareTrafficDevicesDeviceCreatedCompleted = "Countware.Traffic.Devices.DeviceCreatedCompleted";
        public const string CountwareTrafficDevicesDeviceCreatedRejected = "Countware.Traffic.Devices.DeviceCreatedRejected";
        public const string CountwareTrafficDevicesDeviceChangedRejected = "Countware.Traffic.Devices.DeviceChangedRejected";
        public const string CountwareTrafficDevicesDeviceDeletedRejected = "Countware.Traffic.Devices.DeviceDeletedRejected";

        //Event
        public const string CountwareTrafficEventsEventCreated = "Countware.Traffic.Events.EventCreated";
        public static string CountwareTrafficEventsDeviceEventsListener = "Countware.Traffic.Events.<<{0}>>.DeviceEventsListener";
        public const string CountwareTrafficEventsAutoCreateDeviceEventsConsumer = "Countware.Traffic.Events.AutoCreateDeviceEventsConsumer";
        public const string CountwareTrafficEventsAutoDeleteDeviceEventsConsumer = "Countware.Traffic.Events.AutoDeleteDeviceEventsConsumer";
        public const string CountwareTrafficEventsAutoCreateDeviceEventsJob = "Countware.Traffic.Events.AutoCreateDeviceEventsJob";
        public const string CountwareTrafficEventsAutoDeleteDeviceEventsJob = "Countware.Traffic.Events.AutoDeleteDeviceEventsJob";

        //Sms
        public const string CountwareTrafficSendTemplatedSms = "Countware.Traffic.Sms.SendTemplatedSms";
        public const string CountwareTrafficSendSms = "Countware.Traffic.Sms.SendSms";

        //Mail
        public const string CountwareTrafficSendTemplatedEmail = "Countware.Traffic.Email.SendTemplatedEmail";
        public const string CountwareTrafficSendEmail = "Countware.Traffic.Email.SendEmail";

        //PushNotification
        public const string CountwareTrafficSendTemplatedPushNotification = "Countware.Traffic.PushNotification.SendTemplatedPushNotification";
        public const string CountwareTrafficSendPushNotification = "Countware.Traffic.PushNotification.SendPushNotification";

        //SignalRHub
        public const string CountwareTrafficSignalRSubAreaCreatedSuccessfully = "Countware.Traffic.SignalRHub.SubAreaCreatedSuccessfully";
        public const string CountwareTrafficSignalRSubAreaCreatedFailed = "Countware.Traffic.SignalRHub.SubAreaCreatedFailed";
        public const string CountwareTrafficSignalRSubAreaChangedSuccessfully = "Countware.Traffic.SignalRHub.SubAreaChangedSuccessfully";
        public const string CountwareTrafficSignalRSubAreaChangedFailed = "Countware.Traffic.SignalRHub.SubAreaChangedFailed";
        public const string CountwareTrafficSignalRSubAreaDeletedSuccessfully = "Countware.Traffic.SignalRHub.SubAreaDeletedSuccessfully";
        public const string CountwareTrafficSignalRSubAreaDeletedFailed = "Countware.Traffic.SignalRHub.SubAreaDeletedFailed";

        public const string CountwareTrafficSignalRDeviceCreatedSuccessfully = "Countware.Traffic.SignalRHub.DeviceCreatedSuccessfully";
        public const string CountwareTrafficSignalRDeviceCreatedFailed = "Countware.Traffic.SignalRHub.DeviceCreatedFailed";
        public const string CountwareTrafficSignalRDeviceChangedSuccessfully = "Countware.Traffic.SignalRHub.DeviceChangedSuccessfully";
        public const string CountwareTrafficSignalRDeviceChangedFailed = "Countware.Traffic.SignalRHub.DeviceChangedFailed";
        public const string CountwareTrafficSignalRDeviceDeletedSuccessfully = "Countware.Traffic.SignalRHub.DeviceDeletedSuccessfully";
        public const string CountwareTrafficSignalRDeviceDeletedFailed = "Countware.Traffic.SignalRHub.DeviceDeletedFailed";

        //Audit
        public const string CountwareTrafficAudit = "Countware.Traffic.Audit";
        #endregion CountwareTraffic

    }
}
