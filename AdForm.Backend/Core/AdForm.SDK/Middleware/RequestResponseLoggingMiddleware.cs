using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdForm.SDK
{
    /// <summary>
    /// Middleware for logging request and response.
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Create new instance of <see cref="RequestResponseLoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next request delegate.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <param name="correlationContextAccessor">CorrelationContext accessor.</param>
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Process request.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <returns>Returns nothing.</returns>
        public async Task Invoke(HttpContext context)
        {

            var correlationId = _httpContextAccessor?.HttpContext?.GetCorrelationId();
            if (!string.IsNullOrEmpty(correlationId))
            {
                await LogRequest(context, correlationId);
                await LogResponse(context, correlationId);
            }
        }

        private async Task LogRequest(HttpContext context, string correlationId)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}" +
                                   $"Correlation Id: {correlationId}");
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context, string correlationId)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}" +
                                   $"Correlation Id: {correlationId}");

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}
