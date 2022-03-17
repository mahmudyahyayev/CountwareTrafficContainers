using Mhd.Framework.Core;
using System.Collections.Generic;

namespace CountwareTraffic.Services.Areas.Core
{
    public class InvalidCurrencyCodeException : DomainException
    {
        public string CurrencyCode { get; }
        public InvalidCurrencyCodeException(string currencyCode)
            : base(new List<ErrorResult>() { new ErrorResult($"CurrencyCode code must be 3 character. Current: {currencyCode}") }, 400, ResponseMessageType.Error)
        {
            CurrencyCode = currencyCode;
        }
    }
}
