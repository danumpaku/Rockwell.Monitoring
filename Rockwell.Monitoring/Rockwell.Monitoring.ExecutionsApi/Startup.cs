using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rockwell.Monitoring.Database;
using Rockwell.Monitoring.ExecutionsApi.Services;
using Rockwell.Monitoring.ExecutionsApi.Services.Interfaces;
using System;

namespace Rockwell.Monitoring.ExecutionsApi
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
            services.AddDbContext<MonitoringContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("MonitoringDatabase")));

            string schedulerUrl = Configuration.GetValue<string>("SchedulerUrl");
            services.AddHttpClient("scheduler", c => c.BaseAddress = new Uri(schedulerUrl));

            services.AddScoped<IExecutionsService, ExecutionsService>();
            services.AddScoped<IJobsService, JobsService>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.
                            AllowAnyOrigin().
                            AllowAnyMethod().
                            AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rockwell.Monitoring.ExecutionsApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rockwell.Monitoring.ExecutionsApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
