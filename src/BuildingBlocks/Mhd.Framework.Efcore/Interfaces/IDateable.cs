using System;

namespace Mhd.Framework.Efcore
{
    public interface IDateable : IInterceptor
    {
        DateTime AuditCreateDate { get; set; }
        DateTime AuditModifiedDate { get; set; }
    }

}
