using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ToDoApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((builderContext,config) =>
                 {
                     IHostEnvironment env = builderContext.HostingEnvironment;
                     config.SetBasePath(env.ContentRootPath);
                     config.AddJsonFile("kv/appsettings.AdFormAssignment.Kv.json", optional: true);
                     config.Build();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
