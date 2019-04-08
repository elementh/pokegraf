using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Pokegraf.Common.IoC.Configuration
{
    public static class ConfigurationExtension
    {
        public static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        
        public static ILogger LoadLogger(IConfiguration configuration, bool useElasticSearch = false)
        {
            var loggerConf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console();

            if (useElasticSearch)
            {
                var elasticUri = new Uri(configuration.GetConnectionString("ElasticSearch"));
                
                loggerConf.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "log-pokegraf-{{0:yyyy.MM.dd}"
                });
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                loggerConf.MinimumLevel.Information();
            }
            else
            {
                loggerConf.MinimumLevel.Warning();
            }

            return loggerConf.CreateLogger();
        }
    }
}