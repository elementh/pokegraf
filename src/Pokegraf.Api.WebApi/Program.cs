using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Pokegraf.Api.WebApi.Configuration;
using Serilog;

namespace Pokegraf.Api.WebApi
{
    public class Program
    {
        private static IConfiguration Configuration { get; } = ConfigurationExtension.LoadConfiguration();

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
        
        public static void Main(string[] args)
        {
            Log.Logger = ConfigurationExtension.LoadLogger(Configuration);

            try
            {
                Log.Information("Starting Pokegraf");

                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Pokegraf terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}