using Grpc.Core;
using System.Net;

namespace Mhd.Framework.Grpc.Common
{	public static class StatusCodeConverter
    {
		public static HttpStatusCode ConvertToHttpStatusCode(StatusCode grpcStatusCode) 
		{
			switch (grpcStatusCode)
			{
				case StatusCode.OK:
					return HttpStatusCode.OK;

				case StatusCode.Cancelled:
					return HttpStatusCode.RequestTimeout;

				case StatusCode.Unknown:
					return HttpStatusCode.InternalServerError;

				case StatusCode.InvalidArgument:
					return HttpStatusCode.BadRequest;

				case StatusCode.DeadlineExceeded:
					return HttpStatusCode.GatewayTimeout;

				case StatusCode.NotFound:
					return HttpStatusCode.NotFound;

				case StatusCode.AlreadyExists:
					return HttpStatusCode.Conflict;

				case StatusCode.PermissionDenied:
					return HttpStatusCode.Forbidden;

				case StatusCode.Unauthenticated:
					return HttpStatusCode.Unauthorized;

				case StatusCode.ResourceExhausted:
					return HttpStatusCode.TooManyRequests;

				case StatusCode.FailedPrecondition:
					return HttpStatusCode.BadRequest;

				case StatusCode.Aborted:
					return HttpStatusCode.Conflict;

				case StatusCode.OutOfRange:
					return HttpStatusCode.BadRequest;

				case StatusCode.Unimplemented:
					return HttpStatusCode.NotImplemented;

				case StatusCode.Internal:
					return HttpStatusCode.InternalServerError;

				case StatusCode.Unavailable:
					return HttpStatusCode.ServiceUnavailable;

				case StatusCode.DataLoss:
					return HttpStatusCode.InternalServerError;

				default:
					return HttpStatusCode.InternalServerError;
			}
		}

        public static StatusCode ConvertToGrpcCode(HttpStatusCode httpStatusCode)
        {
			switch (httpStatusCode)
			{
				case HttpStatusCode.OK:
					return StatusCode.OK;

				case HttpStatusCode.RequestTimeout:
					return StatusCode.Cancelled;

				case HttpStatusCode.BadRequest:
					return StatusCode.InvalidArgument;

				case HttpStatusCode.GatewayTimeout:
					return StatusCode.DeadlineExceeded;

				case HttpStatusCode.NotFound:
					return StatusCode.NotFound;

				case HttpStatusCode.Forbidden:
					return StatusCode.PermissionDenied;

				case HttpStatusCode.Unauthorized:
					return StatusCode.Unauthenticated;

				case HttpStatusCode.TooManyRequests:
					return StatusCode.ResourceExhausted;

				case HttpStatusCode.Conflict:
					return StatusCode.AlreadyExists;

				case HttpStatusCode.NotImplemented:
					return StatusCode.Unimplemented;

				case HttpStatusCode.InternalServerError:
					return StatusCode.Internal;

				case HttpStatusCode.ServiceUnavailable:
					return StatusCode.Unavailable;

				default:
					return StatusCode.Unknown;
			}
		}
    }
}
