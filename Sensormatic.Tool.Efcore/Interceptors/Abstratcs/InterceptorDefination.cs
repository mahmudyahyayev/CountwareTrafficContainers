using System;
using System.Collections.Generic;

namespace Sensormatic.Tool.Efcore
{
    public abstract class InterceptorDefination
    {
        public IDictionary<Type, Func<IInterceptorGenerator>> Interceptors { get; }
        public abstract Guid GetUserId();

        public event ContextEntitySaveChangeAuditableHandler OnAuditBeforeDelete;
        public event ContextEntitySaveChangeAuditableHandler OnAuditBeforeInsert;
        public event ContextEntitySaveChangeAuditableHandler OnAuditBeforeUpdate;
        public event ContextEntitySaveChangeAuditableFailedHandler OnAuditAfterError;
        public event ContextEntitySaveChangeAuditableSuccessHandler OnAuditAfterSuccess;

        public event ContextEntitySaveChangeDomainEventRaiseHandler OnDomainEventRaiseInsert;
        public event ContextEntitySaveChangeDomainEventRaiseHandler OnDomainEventRaiseUpdate;
        public event ContextEntitySaveChangeDomainEventRaiseHandler OnDomainEventRaiseDelete;
        public event DomainEventRaiseAfterSuccessHandler OnDomainEventRaiseAfterSuccess;
        public event DomainEventRaiseAfterFailedHandler OnDomainEventRaiseAfterError;

        public InterceptorDefination()
        {
            Interceptors = new Dictionary<Type, Func<IInterceptorGenerator>>
            {
                { typeof(IDeletable), () => new DeletableInterceptor() },
                { typeof(ITraceable), () => new TraceableInterceptor(GetUserId()) },
                { typeof(IDateable), () => new DateableInterceptor() },
                { typeof(IAuditable), () => new AuditableInterceptor(
                                                                        OnAuditBeforeDelete,
                                                                        OnAuditBeforeInsert,
                                                                        OnAuditBeforeUpdate,
                                                                        OnAuditAfterSuccess,
                                                                        OnAuditAfterError
                                                                     )
                },
                { typeof(IDomainEventRaisable), () => new DomainEventRaisableInterceptor(   OnDomainEventRaiseDelete,
                                                                                            OnDomainEventRaiseInsert,
                                                                                            OnDomainEventRaiseUpdate,
                                                                                            OnDomainEventRaiseAfterSuccess,
                                                                                            OnDomainEventRaiseAfterError
                                                                                        )
                }
            };
        }
    }
}
