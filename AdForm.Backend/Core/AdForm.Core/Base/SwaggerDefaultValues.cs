using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace AdForm.Core
{
    /// <summary>
    /// Add required header parameters.
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = HttpRequestHeaders.CorrelationId,
                    In = ParameterLocation.Header,
                    Description = HttpRequestHeaders.CorrelationId,
                    Required = false
                });
            }
        }
    }
}
