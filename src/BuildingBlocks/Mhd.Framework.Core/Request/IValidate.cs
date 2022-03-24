using System.Collections.Generic;

namespace Mhd.Framework.Core
{
    public interface IValidate
    {
        bool IsValid { get; }
        ICollection<ErrorResult> ValidateResults { get; }
    }
}
