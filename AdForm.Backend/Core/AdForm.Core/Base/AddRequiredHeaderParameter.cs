using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace AdForm.Core
{
    /// <summary>
    /// Add required header parameters.
    /// </summary>
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HttpRequestHeaders.CorrelationId,
                In = ParameterLocation.Header,
                Description = HttpRequestHeaders.CorrelationId,
                Required = false
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HttpRequestHeaders.Accept,
                In = ParameterLocation.Header,
                Description = HttpRequestHeaders.Accept,
                Required = false
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HttpRequestHeaders.ContentLocation,
                In = ParameterLocation.Header,
                Description = HttpRequestHeaders.ContentLocation,
                Required = false
            });
        }
    }
}
