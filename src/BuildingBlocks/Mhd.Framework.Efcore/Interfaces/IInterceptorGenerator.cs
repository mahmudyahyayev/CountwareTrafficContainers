using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Mhd.Framework.Efcore
{
    public interface IInterceptorGenerator
    {
        void OnBefore(EntityEntry item, DbContext objectContext);
        void OnAfter();
        void OnError(Exception exception);
    }
}
