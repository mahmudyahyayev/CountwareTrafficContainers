using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener
{
    public class AutoCreateDeviceEventsEndpointConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceCreatedCompleted>, ITransientSelfDependency
    {
        public AutoCreateDeviceEventsEndpointConsumer()
        {
        }

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceCreatedCompleted queuEvent)
        {
            //_app.Post<EventLog>($"api/v1/events/{queuEvent.Name}",
            //        beforeDispatch: (eventLog, ctx) =>
            //        {
            //            eventLog.DeviceId = queuEvent.DeviceId;
            //            eventLog.DeviceName = queuEvent.Name;
            //            return Task.CompletedTask;
            //        },
            //        afterDispatch: (cmd, ctx) => ctx.Response.Created());

        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
