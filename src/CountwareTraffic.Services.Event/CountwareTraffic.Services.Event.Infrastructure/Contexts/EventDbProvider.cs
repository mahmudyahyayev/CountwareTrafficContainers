using System;
using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Commands;
using Countware.Traffic.AuditLog;
using CountwareTraffic.Services.Events.Application;
using Mhd.Framework.Common;
using Mhd.Framework.Efcore;
using Mhd.Framework.Ioc;
using Mhd.Framework.Queue;
using Mhd.Framework.QueueModel;

namespace CountwareTraffic.Services.Events.Infrastructure
{
    public class EventDbProvider : InterceptorDefination, IScopedSelfDependency
    {
        private readonly ICollection<Audit> _audits;
        private readonly ICollection<IDomainEventRaisable> _domainEventRaisableList;
        private readonly IQueueService _queueService;
        private readonly IQueueEventMapper _queueEventMapper;
        private readonly IIdentityService _identityService;
        private readonly ICommandDispatcher _commandDispatcher;
        public EventDbProvider(IQueueEventMapper queueEventMapper, IIdentityService identityService, IQueueService queueService, ICommandDispatcher commandDispatcher)
        {
            _queueService = queueService;
            _queueEventMapper = queueEventMapper;
            _identityService = identityService;
            _audits = new List<Audit>();
            _domainEventRaisableList = new List<IDomainEventRaisable>();
            _commandDispatcher = commandDispatcher;

            #region Auditable
            OnAuditBeforeDelete += (item, entityEntry, dbContext) => { _audits.Add(AuditTrailHelper.AuditTrailFactory(dbContext, entityEntry, GetUserId())); };
            OnAuditBeforeInsert += (item, entityEntry, dbContext) => { _audits.Add(AuditTrailHelper.AuditTrailFactory(dbContext, entityEntry, GetUserId())); };
            OnAuditBeforeUpdate += (item, entityEntry, dbContext) => { _audits.Add(AuditTrailHelper.AuditTrailFactory(dbContext, entityEntry, GetUserId())); };
            OnAuditAfterError += (exMessage) => { _audits.Clear(); };

            OnAuditAfterSuccess += () =>
            {
                _queueService.Send(Queues.CountwareTrafficAudit, new AuditList<Audit>() { Items = _audits.ToList(), UserId = GetUserId() });

                _audits.Clear();
            };
            #endregion Auditable


            #region Domain event Raisable
            OnDomainEventRaiseDelete += (item, entityEntry, dbContext) => { _domainEventRaisableList.Add(item); };
            OnDomainEventRaiseInsert += (item, entityEntry, dbContext) => { _domainEventRaisableList.Add(item); };
            OnDomainEventRaiseUpdate += (item, entityEntry, dbContext) => { _domainEventRaisableList.Add(item); };
            OnDomainEventRaiseAfterError += (exMessage) => { _domainEventRaisableList.Clear(); };

            OnDomainEventRaiseAfterSuccess += () =>
            {
                foreach (var raisable in _domainEventRaisableList)
                {
                    var domainEvents = _queueEventMapper.MapAll(raisable.Events, GetUserId());

                    foreach (var domainEvet in domainEvents)
                    {
                        string message = null;

                        try { _queueService.Publish(domainEvet); }
                        catch (Exception ex) { message = ex.Message; }

                        finally
                        {
                            _commandDispatcher.SendAsync(new ProcessOutbox
                            {
                                ExceptionMessage = message,
                                RecordId = domainEvet.RecordId
                            });
                        }
                    }
                }
                _domainEventRaisableList.Clear();
            };
            #endregion Domain event Raisable

        }

        public void Dispose() => GC.SuppressFinalize(this);
        public override Guid GetUserId()
        {
            return _identityService.UserId;
        }
    }
}
