using System;
using System.Collections.Generic;
using System.Linq;

namespace Mhd.Framework.Core
{
    public abstract class SensormaticRequestValidate : ISensormaticRequest, ISensormaticValidator
    {
        public SensormaticRequestValidate() => ValidateResults = new HashSet<ErrorResult>();

        [DoNotSerialize]
        public bool IsValid => !ValidateResults.Any();

        [DoNotSerialize]
        public ICollection<ErrorResult> ValidateResults { get; }

        public abstract void Validate();
    }
}
