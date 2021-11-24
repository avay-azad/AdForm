using AdForm.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Reflection;

namespace ToDoApp.Api
{
    public static class SwaggerServiceExtension
    {
        /// <summary>
        /// implements extension method for adding Sqagger services.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(p =>
            {
                p.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                p.AddSecurityDefinition(GlobalConstants.Bearer, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = HttpRequestHeaders.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = GlobalConstants.Bearer
                });
                p.OperationFilter<SwaggerDefaultValues>();
                p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = GlobalConstants.Bearer
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                p.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }
    }
}
