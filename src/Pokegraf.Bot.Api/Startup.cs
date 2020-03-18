using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Configuration;
using Pokegraf.Core.Domain.Actions.Command.About;
using Pokegraf.Core.Domain.Actions.Command.Start;
using Pokegraf.Core.Domain.Stats.Service;
using Pokegraf.Core.Entity;
using Pokegraf.Infrastructure.Contract.Service;
using Pokegraf.Infrastructure.Implementation.Service;
using Pokegraf.Persistence.Context;

namespace Pokegraf.Bot.Api
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddMediatR(typeof(StartCommandAction).Assembly);

            services.AddScoped<IGlobalStatsService, GlobalStatsService>();

            #region Infrastructure

            services.AddScoped<IPokemonService, PokemonService>();

            #endregion
            
            #region Cache

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration["POKEGRAF_REDIS_CACHE_URL"];
                    options.InstanceName = "pokegraf_cache:";
                });
            }

            #endregion

            #region Navigator

            services.AddNavigator(options =>
            {
                options.BotToken = Configuration["BOT_TOKEN"];
                options.BaseWebHookUrl = Configuration["BASE_WEBHOOK_URL"];
            }, typeof(StartCommandAction).Assembly);

            services.AddNavigatorStore<PokegrafDbContext, Trainer>(
                builder => { builder.UseNpgsql(Configuration["CONNECTION_STRING"], b => b.MigrationsAssembly("Pokegraf.Persistence.Migration")); },
                options => { options.SeUserMapper<TrainerMapper>(); });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PokegrafDbContext>().Database.Migrate();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapNavigator();
            });
        }
    }
}