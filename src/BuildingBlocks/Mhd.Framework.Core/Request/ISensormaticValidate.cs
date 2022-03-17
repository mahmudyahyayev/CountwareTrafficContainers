using System.Collections.Generic;

namespace Mhd.Framework.Core
{
    public interface ISensormaticValidate
    {
        bool IsValid { get; }
        ICollection<ErrorResult> ValidateResults { get; }
    }
}
