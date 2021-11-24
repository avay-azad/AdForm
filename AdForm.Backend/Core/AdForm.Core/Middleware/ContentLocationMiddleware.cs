using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AdForm.Core
{
    public class ContentLocationMiddleware
    {

        private readonly RequestDelegate _next;

        public ContentLocationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<ContentLocationMiddleware>();
            _next = next;
        }

        /// <summary>
        /// Process request.
        /// </summary>
        /// <returns>Returns nothing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                int responseStatusCode = context.Response.StatusCode;
                if (responseStatusCode == (int)HttpStatusCode.Created)
                {
                    IHeaderDictionary headers = context.Response.Headers;
                    StringValues locationHeaderValue = string.Empty;
                    if (headers.TryGetValue(HttpRequestHeaders.ContentLocation, out locationHeaderValue))
                    {
                        context.Response.Headers.Remove(HttpRequestHeaders.ContentLocation);
                        context.Response.Headers.Add(HttpRequestHeaders.ContentLocation, context.Response.Headers[HttpRequestHeaders.Location]);
                    }
                    else
                    {
                        context.Response.Headers.Add(HttpRequestHeaders.ContentLocation, context.Response.Headers[HttpRequestHeaders.Location]);
                    }
                }
                return Task.FromResult(0);
            });
            await _next(context);
        }
    }
    /// <summary>
    /// Extension of application builder for exception middleware.
    /// </summary>
    public static class ContentLocationMiddlewareExtensions
    {
        /// <summary>
        /// Configure Content-Location middleware.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public static IApplicationBuilder UseContentLocationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ContentLocationMiddleware>();
        }

    }
}
