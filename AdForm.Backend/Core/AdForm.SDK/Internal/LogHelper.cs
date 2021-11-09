using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AdForm.SDK
{
    public class LogHelper<T>
    {
        private readonly ILogger<T> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogHelper(ILogger<T> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogEntryInfo(string requestMethod, string requestAssembly, params object[] request)
        {
            _logger.LogInformation(LogTemplate.EntryMessageTemplate, requestAssembly,
               requestMethod, _httpContextAccessor?.HttpContext?.GetCorrelationId(),
               Utility.Serialize(request), DateTime.UtcNow);
        }

        public void LogExitInfo(string requestMethod, string requestAssembly, long start, object response)
        {
            _logger.LogInformation(LogTemplate.ExitMessageTemplateWithResponse, requestAssembly,
                requestMethod, _httpContextAccessor?.HttpContext?.GetCorrelationId(),
                Utility.Serialize(response), DateTime.UtcNow,
                Utility.GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()));
        }

        public void LogException(Exception exception, string statusCode)
        {
            _logger.LogError(LogTemplate.ExceptionMessageTemplate, exception.Message, exception.StackTrace,
                _httpContextAccessor?.HttpContext?.GetCorrelationId(), statusCode, DateTime.UtcNow);
        }

        public void LogInformation(string requestMethod, string requestAssembly, object response)
        {
            _logger.LogInformation(LogTemplate.InformationMessageTemplate, requestAssembly,
                requestMethod, _httpContextAccessor?.HttpContext?.GetCorrelationId(), response, DateTime.UtcNow);
        }

        public void LogInformation(string messageTemplate, string requestMethod, string requestAssembly, string responseCode, string responseDescription)
        {
            _logger.LogInformation(messageTemplate, requestAssembly,
                requestMethod, _httpContextAccessor?.HttpContext?.GetCorrelationId(), responseCode, responseDescription, DateTime.UtcNow);
        }

        public void LogWarning(string requestMethod, string requestAssembly, object response)
        {
            _logger.LogInformation(LogTemplate.WarningMessageTemplate, requestAssembly,
                requestMethod, _httpContextAccessor?.HttpContext?.GetCorrelationId(), response, DateTime.UtcNow);
        }

        public void LogDebug(string requestMethod, string requestAssembly, object response)
        {
            _logger.LogDebug(LogTemplate.DebugLocalVariableTemplate, requestAssembly,
                    requestMethod, _httpContextAccessor?.HttpContext?.GetCorrelationId(),
                    Utility.Serialize(response), DateTime.UtcNow);
        }
    }
}
