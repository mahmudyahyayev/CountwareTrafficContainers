using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sensormatic.Tool.Efcore
{
    public delegate void ContextEntitySaveChangeAuditableHandler(IAuditable item, EntityEntry entityEntry, DbContext dbContext);
    public delegate void ContextEntitySaveChangeAuditableFailedHandler(string exceptionMessage);
    public delegate void ContextEntitySaveChangeAuditableSuccessHandler();

    public delegate void ContextEntitySaveChangeDomainEventRaiseHandler(IDomainEventRaisable item, EntityEntry entityEntry, DbContext dbContext);
    public delegate void DomainEventRaiseAfterSuccessHandler();
    public delegate void DomainEventRaiseAfterFailedHandler(string exceptionMessage);
}
