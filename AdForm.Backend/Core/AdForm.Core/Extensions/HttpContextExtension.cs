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

        //public static string GetLanguage(this HttpContext httpContext)
        //{
        //    httpContext.Request.Headers.TryGetValue(HttpRequestHeaders.AcceptLanguage, out StringValues language);
        //    string locale = language.FirstOrDefault() ?? GlobalConstants.Culture_English;
        //    return locale.Contains(GlobalConstants.Culture_Arabic) ? GlobalConstants.Culture_Arabic : GlobalConstants.Culture_English;
        //}

        //public static string GetAppVersion(this HttpContext httpContext)
        //{
        //    string appVersion = httpContext.GetUserAgent();
        //    if (appVersion.Contains(GlobalConstants.ForwardSlash))
        //    {
        //        appVersion = appVersion.Split(GlobalConstants.ForwardSlash)[1].ToString();
        //        if (appVersion.Contains(GlobalConstants.SmallOpenBracket))
        //        {
        //            appVersion = appVersion.Split(GlobalConstants.SmallOpenBracket)[0].ToString();
        //        }
        //    }

        //    return appVersion;
        //}

//        public static string GetMetricsCurrentResourceName(this HttpContext httpContext)
//        {
//            if (httpContext == null)
//                throw new ArgumentNullException(nameof(httpContext));

//            Endpoint endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;

//#if NETCOREAPP3_1
//            return endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
//#else
//                return endpoint?.Metadata.GetMetadata<IRouteValuesAddressMetadata>()?.RouteName;
//#endif
//        }
   }
}
