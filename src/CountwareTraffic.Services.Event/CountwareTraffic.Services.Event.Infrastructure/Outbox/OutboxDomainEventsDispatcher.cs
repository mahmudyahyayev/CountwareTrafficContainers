using CountwareTraffic.Services.Events.Application;
using Mhd.Framework.Efcore;
using Mhd.Framework.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public interface IOutboxIDomainEventsDispatcher : IScopedDependency
    {
        Task DispatchEventsAsync();
    }

    public class OutboxDomainEventsDispatcher : IOutboxIDomainEventsDispatcher
    {
        private readonly EventDbContext _eventDbContext;
        private readonly IQueueEventMapper _queueEventMapper;
        private readonly IIdentityService _identityService;
        public OutboxDomainEventsDispatcher(EventDbContext deviceDbContext, IQueueEventMapper queueEventMapper, IIdentityService identityService)
        {
            _eventDbContext = deviceDbContext;
            _queueEventMapper = queueEventMapper;
            _identityService = identityService;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._eventDbContext.ChangeTracker
                .Entries<IDomainEventRaisable>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events)
                .ToList();

            var mappedEvents = _queueEventMapper.MapAll(domainEvents, _identityService.UserId);

            foreach (var queueEvent in mappedEvents)
            {
                OutboxMessage outboxMessage = new(DateTime.Now, queueEvent.GetType().FullName, Newtonsoft.Json.JsonConvert.SerializeObject(queueEvent), queueEvent.RecordId);
                _eventDbContext.OutboxMessages.Add(outboxMessage);
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
