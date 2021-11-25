using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdForm.Core
{
    public static class SerilogServiceExtension
    {
        /// <summary>
        /// implements extension method for adding serilog services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var serilog = new SerilogLogging();
            serilog.Loging(services, configuration);
            return services;
        }
    }
}
