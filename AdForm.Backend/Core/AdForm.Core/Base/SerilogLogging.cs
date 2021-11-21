using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace AdForm.Core
{
    public class SerilogLogging
    {
        public void Loging(IServiceCollection services, IConfiguration _configuration)
        {
            try
            {
                var serilogLogger = new LoggerConfiguration()
               .ReadFrom.Configuration(_configuration)
               .Enrich.FromLogContext()
               .Enrich.WithCorrelationId()
               .Enrich.WithCorrelationIdHeader(HttpRequestHeaders.CorrelationId)
               .Enrich.WithMachineName()
               .CreateLogger();
                services.AddLogging(loggingbuilder =>
                {
                    loggingbuilder.ClearProviders();
                    loggingbuilder.AddSerilog(serilogLogger);
                });

                Log.Information("Starting up");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
         
    }
}
