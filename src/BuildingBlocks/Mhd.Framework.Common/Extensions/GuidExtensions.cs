using System;

namespace Mhd.Framework.Common
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid target) => target == Guid.Empty;

        public static bool IsNullOrEmpty(this Guid? target) => (target is null || target == Guid.Empty);
    }
}
