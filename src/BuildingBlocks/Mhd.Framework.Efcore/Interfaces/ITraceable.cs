using System;

namespace Mhd.Framework.Efcore
{
    public interface ITraceable : IInterceptor
    {
        Guid AuditCreateBy { get; set; }
        Guid AuditModifiedBy { get; set; }
    }
}
