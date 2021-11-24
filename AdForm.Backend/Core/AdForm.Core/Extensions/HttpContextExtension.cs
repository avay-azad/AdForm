using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace AdForm.Core
{
    public static class HttpContextExtension
    {
        public static string GetCorrelationId(this HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(HttpRequestHeaders.CorrelationId, out StringValues correlationId);
            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }

        public static string GetDeviceId(this HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(HttpRequestHeaders.DeviceId, out StringValues deviceId);
            return deviceId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }

        public static string GetUserAgent(this HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(HttpRequestHeaders.UserAgent, out StringValues userAgent);
            return userAgent.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }

        public static string GetAuthorizationToken(this HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(HttpRequestHeaders.Authorization, out StringValues authorizationToken);
            return authorizationToken.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }
   }
}
