using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;

namespace Mhd.Framework.Core
{
    public class ErrorModel
    {
        public ErrorModel(ICollection<ErrorResult> errorResults, int code, ResponseMessageType type)
        {
            Type = type.ToString();
            Code = code;
            ErrorResults = errorResults;
        }

        public string Type { get; set; }
        [JsonIgnore]
        public int Code { get; set; }
        public ICollection<ErrorResult> ErrorResults { get; set; }
        public string Message => ErrorResults.Count > 0 ? string.Join(Environment.NewLine, ErrorResults.Select(s => s.Message)) : null;
    }

    public class ErrorResult
    {
        public string Field { get; set; }

        public string Message { get; set; }

        public ErrorResult(string message, string field = null)
        {
            Message = message;
            Field = field;
        }
    }
}