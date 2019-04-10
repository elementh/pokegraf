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
        
        public static ILogger LoadLogger(IConfiguration configuration)
        {
            var loggerConf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console();

            var elasticUri = GetElasticUri(configuration);
            
            if (elasticUri != null)
            {
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

        private static Uri GetElasticUri(IConfiguration configuration)
        {
            var elasticUri = string.IsNullOrWhiteSpace(configuration["POKEGRAF_ELASTICSEARCH_URL"]) 
                ? new Uri(configuration.GetConnectionString("ElasticSearch")) 
                : new Uri(configuration["POKEGRAF_ELASTICSEARCH_URL"]);

            return elasticUri;
        }
    }
}