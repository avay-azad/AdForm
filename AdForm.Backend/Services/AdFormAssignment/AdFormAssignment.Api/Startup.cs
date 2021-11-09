using AdForm.DBService;
using AdForm.SDK;
using AdFormAssignment.Business;
using AdFormAssignment.DataService;
using AdFormAssignment.Shared;
using AutoMapper;
using FluentValidation;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Text;

namespace AdFormAssignment.Api
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

            services.AddControllers(p => p.RespectBrowserAcceptHeader = true).AddXmlDataContractSerializerFormatters()
                .AddNewtonsoftJson();
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
            AdddAdFormServices(services);
            RegisterSwagger(services, AppConstants.SwaggerVersion, AppConstants.SwaggerTitle,
                AppConstants.SwaggerDescription, AppConstants.SwaggerDocumentName);

            ConfigureAuthentication(services);


            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });


        }

        private void AddAppSettingConfig(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
          
        }
        private void ConfigureAuthentication(IServiceCollection services)
        {
            string secret=  Configuration.GetSection("AppSettings")
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
            services.AddSwaggerDocument(options =>
            {
                options.Version = version;
                options.Title = title;
                options.Description = description;
                options.DocumentName = documentName;
                options.GenerateEnumMappingDescription = true;
                options.RequireParametersWithoutDefault = true;
                options.OperationProcessors.Add(new OperationSecurityScopeProcessor(HttpRequestHeaders.Authorization));
                options.DocumentProcessors.Add(new SecurityDefinitionAppender(HttpRequestHeaders.Authorization, new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = HttpRequestHeaders.Authorization,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = GlobalConstants.BearerToken
                }));
                options.OperationProcessors.Add(new SampleHeaderOperationProcessor());
            });
        }

        private void AddAdFormAssignmentDBServices(IServiceCollection services)
        {
            services.AddDbContextPool<HomeworkDBContext>(option =>
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
        }

        private void AdddAdFormServices(IServiceCollection services)
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

        public virtual void RegisterLogging(IServiceCollection services)
        {
            var serilog = new SerilogLogging();
            serilog.Loging(services, Configuration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HomeworkDBContext>();
                context.Database.EnsureCreated();
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
            app.UseOpenApi();
            app.UseSwaggerUi3();

        }
    }
}
