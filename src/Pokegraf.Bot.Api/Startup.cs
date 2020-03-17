using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Navigator;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Configuration;
using Pokegraf.Core.Domain.Actions.Command.About;
using Pokegraf.Core.Domain.Stats.Service;
using Pokegraf.Core.Entity;
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
            
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddScoped<IGlobalStatsService, GlobalStatsService>();

            #region Navigator

            services.AddNavigator(options =>
            {
                options.BotToken = Configuration["BOT_TOKEN"];
                options.BaseWebHookUrl = Configuration["BASE_WEBHOOK_URL"];
            }, typeof(AboutCommandAction).Assembly);

            services.AddNavigatorStore<PokegrafDbContext, Trainer>(builder =>
                {
                    builder.UseNpgsql(Configuration["CONNECTION_STRING"], b => b.MigrationsAssembly("Pokegraf.Persistence.Migration"));                    
                },
                options =>
                {
                    options.SeUserMapper<TrainerMapper>();
                });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}