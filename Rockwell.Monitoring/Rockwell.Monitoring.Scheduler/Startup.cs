using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Rockwell.Monitoring.Scheduler.JobFactories;
using Rockwell.Monitoring.Scheduler.Services;
using Rockwell.Monitoring.Scheduler.Services.Interfaces;
using System;
using System.Collections.Specialized;

namespace Rockwell.Monitoring.Scheduler
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
            string scraperUrl = Configuration.GetValue<string>("ScraperUrl");
            services.AddHttpClient("scraper", c =>  c.BaseAddress = new Uri(scraperUrl));

            //TODO activar persistencia de jobs (actualmente está generando error por lo que se deja temporalmente la JobStore en memoria)
            services.AddSingleton(new StdSchedulerFactory().GetScheduler().Result);
            //services.AddSingleton(SchedulerBuilder.Create()
            //    .UsePersistentStore(x =>
            //    {
            //        x.UseProperties = true;
            //        x.RetryInterval = TimeSpan.FromSeconds(15);
            //        x.UseClustering();
            //        x.UseMySql(mysql =>
            //        {
            //            mysql.ConnectionString = Configuration.GetConnectionString("QuartzDatabase");
            //            mysql.TablePrefix = "QRTZ_";
            //        });
            //    })
            //    .BuildScheduler().Result);

            services.AddSingleton<IJobFactory, ScrapeJobFactory>();
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IScheduleService, ScheduleService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rockwell.Monitoring.Scheduler", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rockwell.Monitoring.Scheduler v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
