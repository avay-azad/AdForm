using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdForm.Core
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorMessage = string.Empty;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errorType = string.Empty;
            string businessCode = string.Empty;
            var listFieldErrors = new List<ModelErrorResponse>();
            if (exception is AdFormException)
            {
                errorMessage = exception.Message;
                statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode),
                    exception.Data["AdForm:HttpStatusCode"].ToString());

                List<FluentValidation.Results.ValidationFailure> lstError = (List<FluentValidation.Results.ValidationFailure>)exception.Data["ENBD:FieldErrors"];
                if (lstError != null)
                {
                    listFieldErrors = lstError.Select(error => new ModelErrorResponse
                    {
                        Code = error.ErrorCode,
                        Message = error.ErrorMessage
                    }).ToList();
                }

                businessCode = exception.Data["AdForm:ExceptionCode"].ToString();
                errorType = exception.GetType().Name;

                _logger.LogError(LogTemplate.ExceptionMessageTemplate, exception.Message,
                    exception.StackTrace, context.GetCorrelationId(), Convert.ToInt32(statusCode), Stopwatch.GetTimestamp());
            }
            else
            {
                errorMessage = ApiErrorMessage.Global_Error_Message;
                errorType = AdFormExceptionType.ApiException.ToString();
                businessCode = Convert.ToInt32(AdFormExceptionType.ApiException).ToString();
              
               _logger.LogCritical(LogTemplate.ExceptionMessageTemplate, exception.Message, exception.StackTrace, context.GetCorrelationId(), Convert.ToInt32(statusCode), Stopwatch.GetTimestamp());
            }

            var result = JsonSerializer.Serialize(new ApiErrorResponse()
            {
                Error = new Error
                {
                    Type = errorType,
                    Code = businessCode,
                    Message = errorMessage,
                    FieldErrors = listFieldErrors
                }
            }, Utility.GetSerializerOptions());
            context.Response.ContentType = GlobalConstants.ContentType;
            context.Response.StatusCode = Convert.ToInt32(statusCode);
            return context.Response.WriteAsync(result);
        }
    }
}
