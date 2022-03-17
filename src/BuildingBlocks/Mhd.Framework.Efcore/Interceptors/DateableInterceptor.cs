using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Mhd.Framework.Efcore
{
    internal class DateableInterceptor : InterceptorGenerator<IDateable>
    {
        public override void OnAfterError(string execptionMessage) { }

        public override void OnAfterInsert() { }

        public override void OnBeforeDelete(IDateable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditModifiedDate = DateTime.Now;

        public override void OnBeforeInsert(IDateable item, EntityEntry entityEntry, DbContext dbContext)
            => item.AuditCreateDate = item.AuditModifiedDate = DateTime.Now;

        public override void OnBeforeUpdate(IDateable item, EntityEntry entityEntry, DbContext dbContext) 
            => item.AuditModifiedDate = DateTime.Now;
    }
}
