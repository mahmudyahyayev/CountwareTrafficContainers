using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sensormatic.Tool.Core
{
    public interface ISensormaticValidate
    {
        bool IsValid { get; }
        string UniqueValue { get; set; }
        ICollection<ErrorResult> ValidateResults { get; }
    }

    public interface ISensormaticValidator : ISensormaticValidate
    {
        void Validate();
    }

    public abstract class SensormaticRequestValidate : ISensormaticRequest, ISensormaticValidator
    {
        public Guid UserId { get; set; }
        public SensormaticRequestValidate()
        {
            ValidateResults = new HashSet<ErrorResult>();
        }

        [DoNotSerialize]
        public bool IsValid => !ValidateResults.Any();

        [DoNotSerialize]
        public string UniqueValue { get; set; }

      
        [DoNotSerialize]
        public ICollection<ErrorResult> ValidateResults { get; }

        public abstract void Validate();
    }

    public interface ISensormaticRequest
    {
        Guid UserId { get; set; }
    }
}
