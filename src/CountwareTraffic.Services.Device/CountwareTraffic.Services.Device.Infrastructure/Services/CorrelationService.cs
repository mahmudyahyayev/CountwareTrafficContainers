using System;
using Countware.Traffic.Observability;
using CountwareTraffic.Services.Devices.Application;
using Microsoft.AspNetCore.Http;

namespace CountwareTraffic.Services.Devices.Infrastructure.Services
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

                if (string.IsNullOrWhiteSpace(correlationId))
                    correlationId = Guid.NewGuid().ToString("N");

                return correlationId;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
