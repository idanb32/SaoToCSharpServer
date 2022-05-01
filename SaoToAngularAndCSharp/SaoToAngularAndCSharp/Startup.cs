using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SaoToAngularAndCSharp.DAL.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            string allowedUrls = Configuration.GetConnectionString("AllowedUrl");
            var mongoDbSection = Configuration.GetSection("MongoDb");
            services.Configure<MongoDbSettings>(mongoDbSection);
            services.AddSingleton<IMongoDbSettings>(provider =>
            provider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddScoped<IRepostory ,Repostory>();

            services.AddCors(option =>
                option.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(allowedUrls)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                }));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging() || env.IsProduction())
            {
                app.UseExceptionHandler("/Error/Index");
            }
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
