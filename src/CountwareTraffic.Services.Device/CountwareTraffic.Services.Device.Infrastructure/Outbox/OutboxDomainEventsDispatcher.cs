using CountwareTraffic.Services.Devices.Application;
using Mhd.Framework.Efcore;
using Mhd.Framework.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Devices.Infrastructure
{
    public interface IOutboxIDomainEventsDispatcher : IScopedDependency
    {
        Task DispatchEventsAsync();
    }

    public class OutboxDomainEventsDispatcher : IOutboxIDomainEventsDispatcher
    {
        private readonly DeviceDbContext _deviceDbContext;
        private readonly IQueueEventMapper _queueEventMapper;
        private readonly IIdentityService _identityService;
        public OutboxDomainEventsDispatcher(DeviceDbContext deviceDbContext, IQueueEventMapper queueEventMapper, IIdentityService identityService)
        {
            _deviceDbContext = deviceDbContext;
            _queueEventMapper = queueEventMapper;
            _identityService = identityService;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._deviceDbContext.ChangeTracker
                .Entries<IDomainEventRaisable>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events)
                .ToList();

            var mappedEvents = _queueEventMapper.MapAll(domainEvents, _identityService.UserId);

            foreach (var queueEvent in mappedEvents)
            {
                OutboxMessage outboxMessage = new(DateTime.Now, queueEvent.GetType().FullName, Newtonsoft.Json.JsonConvert.SerializeObject(queueEvent), queueEvent.RecordId);
                _deviceDbContext.OutboxMessages.Add(outboxMessage);
            }
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
