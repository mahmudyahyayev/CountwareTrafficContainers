using System;

namespace Mhd.Framework.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DoNotSerializeAttribute : Attribute
    {
    }
}
