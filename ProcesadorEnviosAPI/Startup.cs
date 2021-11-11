using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;

using Microsoft.EntityFrameworkCore;
using ProcesadorEnviosAPI.Models;

namespace ProcesadorEnviosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(
                options => 
                    options.UseMySql(Configuration.GetConnectionString("BDConnectionString"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("BDConnectionString"))
                )
            );

            services.AddControllers();

            services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new OpenApiInfo { Title = "AWS Serverless Asp.Net Core Web API", Version = "v1" });});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>{
                c.SwaggerEndpoint("/Prod/swagger/v1/swagger.json",                             "AWS Serverless Asp.Net Core Web API");
                c.RoutePrefix = "swagger";
            });


        }
    }
}
