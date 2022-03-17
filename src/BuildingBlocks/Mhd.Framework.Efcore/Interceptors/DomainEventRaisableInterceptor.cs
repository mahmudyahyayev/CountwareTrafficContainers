using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mhd.Framework.Efcore
{
    internal class DomainEventRaisableInterceptor : InterceptorGenerator<IDomainEventRaisable>
    {
        private event DomainEventRaiseAfterSuccessHandler _onDomainEventRaiseAfterSuccess;
        private event DomainEventRaiseAfterFailedHandler _onDomainEventRaiseAfterError;

        private event ContextEntitySaveChangeDomainEventRaiseHandler _onDomainEventRaiseDelete;
        private event ContextEntitySaveChangeDomainEventRaiseHandler _onDomainEventRaiseInsert;
        private event ContextEntitySaveChangeDomainEventRaiseHandler _onDomainEventRaiseUpdate;

        public DomainEventRaisableInterceptor(ContextEntitySaveChangeDomainEventRaiseHandler onDomainEventRaiseDelete,
                                              ContextEntitySaveChangeDomainEventRaiseHandler onDomainEventRaiseInsert,
                                              ContextEntitySaveChangeDomainEventRaiseHandler onDomainEventRaiseUpdate,
                                              DomainEventRaiseAfterSuccessHandler onDomainEventRaiseAfterSuccess,
                                              DomainEventRaiseAfterFailedHandler onDomainEventRaiseAfterError)
        {
            _onDomainEventRaiseAfterSuccess = onDomainEventRaiseAfterSuccess;
            _onDomainEventRaiseAfterError = onDomainEventRaiseAfterError;
            _onDomainEventRaiseDelete = onDomainEventRaiseDelete;
            _onDomainEventRaiseInsert = onDomainEventRaiseInsert;
            _onDomainEventRaiseUpdate = onDomainEventRaiseUpdate;
        }

        public override void OnAfterInsert()
            => _onDomainEventRaiseAfterSuccess?
                .Invoke();

        public override void OnAfterError(string exceptionMessage)
            => _onDomainEventRaiseAfterError?
                .Invoke(exceptionMessage);

        public override void OnBeforeDelete(IDomainEventRaisable item, EntityEntry entityEntry, DbContext dbContext)
            => _onDomainEventRaiseDelete?
                .Invoke(item, entityEntry, dbContext);
        public override void OnBeforeInsert(IDomainEventRaisable item, EntityEntry entityEntry, DbContext dbContext)
             => _onDomainEventRaiseInsert?
                 .Invoke(item, entityEntry, dbContext);
        public override void OnBeforeUpdate(IDomainEventRaisable item, EntityEntry entityEntry, DbContext dbContext)
        => _onDomainEventRaiseUpdate?
                 .Invoke(item, entityEntry, dbContext);
    }
}
