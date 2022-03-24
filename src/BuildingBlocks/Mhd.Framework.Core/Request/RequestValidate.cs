using System;
using System.Collections.Generic;
using System.Linq;

namespace Mhd.Framework.Core
{
    public abstract class RequestValidate : IRequest, IValidator
    {
        public RequestValidate() => ValidateResults = new HashSet<ErrorResult>();

        [DoNotSerialize]
        public bool IsValid => !ValidateResults.Any();

        [DoNotSerialize]
        public ICollection<ErrorResult> ValidateResults { get; }

        public abstract void Validate();
    }
}
