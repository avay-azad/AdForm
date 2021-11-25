using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace AdForm.Core
{
    public static class SwaggerServiceExtension
    {
        /// <summary>
        /// implements extension method for adding swagger services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="ApiXmlFileName"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, string ApiXmlFileName)
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
                p.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, ApiXmlFileName));
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
                // note: the specified format code will format the version as "'v'major[.minor][-status]"  
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
                // can also be used to control the format of the API version in route templates  
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            return services;
        }
    }
}
