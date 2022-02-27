using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sensormatic.Tool.Efcore
{
    internal class DeletableInterceptor : InterceptorGenerator<IDeletable>
    {
        public override void OnAfterError(string execptionMessage) { }
        public override void OnAfterInsert() { }
        public override void OnBeforeDelete(IDeletable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditIsDeleted = true;

        public override void OnBeforeInsert(IDeletable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditIsDeleted = false;

        public override void OnBeforeUpdate(IDeletable item, EntityEntry entityEntry, DbContext dbContext) { }
    }
}
