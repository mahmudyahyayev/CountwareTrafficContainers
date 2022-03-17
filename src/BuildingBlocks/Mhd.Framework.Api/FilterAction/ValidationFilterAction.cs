using Microsoft.AspNetCore.Mvc.Filters;
using Mhd.Framework.Core;
using Mhd.Framework.Ioc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Mhd.Framework.Api
{
    public class ValidationFilterAction : IActionFilter, IOrderedFilter, IScopedSelfDependency
    {
        private bool disposedValue;

        public int Order { get; set; }

        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            List<ErrorResult> validates = new();

            foreach (var value in context.ActionArguments.Values)
            {
                var validInfoArray = value as ISensormaticRequest[] ?? new ISensormaticRequest[] { value as ISensormaticRequest }.Where(w => null != w);

                var validArray = value as SensormaticRequestValidate[] ?? new SensormaticRequestValidate[] { value as SensormaticRequestValidate }.Where(w => null != w);

                foreach (var valid in validArray)
                {
                    valid.Validate();

                    if (valid.ValidateResults.Count > 0)
                        validates.AddRange(valid.ValidateResults);
                }

            }
            if (validates.Count > 0)
                throw new RequestException(validates, (int)HttpStatusCode.UnprocessableEntity, ResponseMessageType.ValidationException);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //todo:Not mahmud yahyayev ozel bir durum olussa buraya donup bakacagim :)
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        } 
    }
}
