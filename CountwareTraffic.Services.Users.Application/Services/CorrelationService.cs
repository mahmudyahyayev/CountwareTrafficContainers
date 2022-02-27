using Countware.Traffic.CrossCC.Observability;
using Microsoft.AspNetCore.Http;
using System;

namespace CountwareTraffic.Services.Users.Application
{
    public class CorrelationService : ICorrelationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CorrelationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CorrelationId
        {
            get
            {
                string correlationId = String.Empty;

                if (_httpContextAccessor.HttpContext != null)
                    correlationId = _httpContextAccessor.HttpContext.GetCorrelationId();

                return correlationId;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
