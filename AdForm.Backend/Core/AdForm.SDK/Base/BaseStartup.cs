using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdForm.SDK
{
    public class BaseStartup
    {
        private readonly IConfiguration _configuration;

        public BaseStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
          
            //.AddFluentValidation(fv =>
            //{
            //    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            //});

            RegisterLogging(services);
         //   RegisterApplicationInsightsTelemetry(services);


        }


        public virtual void RegisterAutoMapper<T>(IServiceCollection services)
        {
          //  services.AddAutoMapper(typeof(T));
        }

      

        public virtual void RegisterLogging(IServiceCollection services)
        {
            var serilog = new SerilogLogging();
            serilog.Loging(services, _configuration);
        }

      
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (!env.IsDevelopment())
                app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            //app.UseCors(GlobalConstants.CorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
           // app.UseMiddleware(typeof(CustomAuthorizationMiddleware));
            app.UseMvc();
           // app.UseOpenApi();
           // app.UseSwaggerUi3();
        }

            
    }
}
