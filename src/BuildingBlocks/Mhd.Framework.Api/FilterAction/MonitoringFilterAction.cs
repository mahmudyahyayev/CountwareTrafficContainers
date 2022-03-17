using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using System.Linq;

namespace Mhd.Framework.Api
{
    public class MonitoringFilterAction : IActionFilter, IOrderedFilter, IScopedSelfDependency
    {
        private readonly IMonitorLogManager logManager;

        public event MonitoringResultHandler OnMonitoringResultHandler;

        public int Order { get; set; }

        public MonitoringFilterAction(IMonitorLogManager _logManager)
            => logManager = _logManager;

       
        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            var description = ((ControllerActionDescriptor)context.ActionDescriptor);

            bool hasServiceLog = description.MethodInfo.CustomAttributes.Any(item => item.AttributeType == typeof(ServiceLogAttribute));

            if (logManager.TryCreate(description.ActionName, context.ActionArguments, hasServiceLog, out WebServiceLog SessionInformation))
            {
                context.HttpContext.Items.Add("SessionInformation", SessionInformation);
                context.HttpContext.Items.Add("HasServiceLog", hasServiceLog.ToString());
            }
        }

        public virtual void OnActionExecuted(ActionExecutedContext context)
            => logManager.CompleteLog(context, context.HttpContext.Items, OnMonitoringResultHandler);

        public void Dispose()
            => System.GC.SuppressFinalize(this);
    }
}
