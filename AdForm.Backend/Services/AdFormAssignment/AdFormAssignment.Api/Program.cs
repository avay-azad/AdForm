using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace AdFormAssignment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((builderContext, config) =>
                 {
                     IHostEnvironment env = builderContext.HostingEnvironment;
                     Console.WriteLine($"Environment Name : {env.EnvironmentName}");
                     config.SetBasePath(env.ContentRootPath);
                     config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                     config.AddJsonFile(ConfigMapFileProvider.FromRelativePath("configs"),
                                 $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                     config.AddJsonFile("kv/appsettings.AdFormAssignment.Kv.json", optional: true);
                     Console.WriteLine($"appsettings.{env.EnvironmentName}.json Loaded");
                     config.Build();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
