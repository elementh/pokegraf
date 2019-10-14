using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Pokegraf.Api.WebApi.Configuration
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

            var elasticConnectionString = configuration["POKEGRAF_ELASTICSEARCH_URL"];
            
            if (!string.IsNullOrWhiteSpace(elasticConnectionString))
            {
                var username = configuration["POKEGRAF_ELASTICSEARCH_USERNAME"];
                var password = configuration["POKEGRAF_ELASTICSEARCH_PASSWORD"];
                
                loggerConf.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticConnectionString))
                {
                    ModifyConnectionSettings = (conn) => conn.BasicAuthentication(username, password),
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = "log-pokegraf-{0:yyyy.MM.dd}",
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage:true)
                });
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                loggerConf.MinimumLevel.Debug();
            }
            else
            {
                loggerConf.MinimumLevel.Information();
            }

            return loggerConf.CreateLogger();
        }
    }
}