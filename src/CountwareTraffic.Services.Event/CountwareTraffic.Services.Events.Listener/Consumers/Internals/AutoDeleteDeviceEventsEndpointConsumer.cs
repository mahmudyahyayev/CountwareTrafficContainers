using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Listener
{
    public class AutoDeleteDeviceEventsEndpointConsumer : IConsumer<Mhd.Framework.QueueModel.DeviceDeletedSuccessfully>, ITransientSelfDependency
    {
        public AutoDeleteDeviceEventsEndpointConsumer()
        {
        
        }

        public async Task ConsumeAsync(Mhd.Framework.QueueModel.DeviceDeletedSuccessfully queuEvent)
        {
          
        }

        public void Dispose() => System.GC.SuppressFinalize(this);
    }
}
