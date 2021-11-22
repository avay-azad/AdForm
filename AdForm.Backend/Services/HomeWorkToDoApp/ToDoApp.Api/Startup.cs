using AdForm.Core;
using AdForm.DBService;
using AutoMapper;
using FluentValidation;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using ToDoApp.Business;
using ToDoApp.DataService;
using ToDoApp.Shared;

namespace ToDoApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            }).AddNewtonsoftJson();
            
            services.Configure<ApiBehaviorOptions>(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });

            services.AddHttpContextAccessor();

            ConfigureGraphQl(services);
            RegisterLogging(services);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            AddAppSettingConfig(services);
            services.AddSingleton(mapper);
            services.AddScoped<IJwtUtils, JwtUtils>();
            AddAdFormAssignmentDBServices(services);
            AddAdFormValidator(services);
            AddAdFormServices(services);
            RegisterSwagger(services, AppConstants.SwaggerVersion, AppConstants.SwaggerTitle,
                AppConstants.SwaggerDescription, AppConstants.SwaggerDocumentName);

            ConfigureAuthentication(services);
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
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HomeworkDBContext>();
                context.Database.Migrate();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(RequestResponseLoggingMiddleware));
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseGraphQL().UsePlayground();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
                c.RoutePrefix = string.Empty;
            });

        }

        //This method gets NewtonsoftJsonPatchInputFormatter for formatting JsonPatch document input.
        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            ServiceProvider builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }
        private void AddAppSettingConfig(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            string secret = Configuration.GetSection("AppSettings")
                            .GetSection("Secret").Value;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });

        }

        private void ConfigureGraphQl(IServiceCollection services)
        {
            services.AddGraphQL(s => SchemaBuilder.New()
               .AddServices(s)
               .AddType<LabelType>()
               .AddType<ItemsType>()
               .AddType<ListsType>()
               .AddQueryType<Query>()
               .AddMutationType<Mutation>()
               .AddAuthorizeDirectiveType()
               .Create());
        }

        public virtual void RegisterSwagger(IServiceCollection services, string version, string title, string description, string documentName)
        {
            services.AddSwaggerGen(p =>
            {
                p.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                p.AddSecurityDefinition(GlobalConstants.Bearer, new OpenApiSecurityScheme
                {
                    Description = description,
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
        }

        private void AddAdFormAssignmentDBServices(IServiceCollection services)
        {
            services.AddDbContext<HomeworkDBContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString(GlobalConstants.AdFormDataContext)));
            services.AddScoped<DbContext, HomeworkDBContext>();
        }

        private void AddAdFormValidator(IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequestDto>, LoginRequestValidator>();
            services.AddTransient<IValidator<ToDoItemRequestDto>, CreateItemRequestValidator>();
            services.AddTransient<IValidator<UpdateToDoItemRequestDto>, UpdateItemRequestValidator>();
            services.AddTransient<IValidator<ToDoListRequestDto>, CreateListRequestValidator>();
            services.AddTransient<IValidator<UpdateToDoListRequestDto>, UpdateListRequestValidator>();
            services.AddTransient<IValidator<LabelRequestDto>, CreateLabelRequestValidator>();
            services.AddTransient<IValidator<AssignLabelRequestDto>, AssignLabelRequestvalidator>();
        }

        private void AddAdFormServices(IServiceCollection services)
        {
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IToDoItemAppService, ToDoItemAppService>();
            services.AddScoped<IToDoItemDataService, ToDoItemDataService>();
            services.AddScoped<IToDoListAppService, ToDoListAppService>();
            services.AddScoped<IToDoListDataService, ToDoListDataService>();
            services.AddScoped<ILableAppService, LableAppService>();
            services.AddScoped<ILabelDataService, LabelDataService>();
        }

        private void RegisterLogging(IServiceCollection services)
        {
            var serilog = new SerilogLogging();
            serilog.Loging(services, Configuration);
        }

    }
}
