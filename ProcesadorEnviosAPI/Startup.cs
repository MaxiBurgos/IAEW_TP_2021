using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string domain = Configuration["Auth0:Domain"];

            services.AddDbContext<ApiContext>(
                options => 
                    options.UseMySql(Configuration.GetConnectionString("BDConnectionString"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("BDConnectionString"))
                )
            );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"] ;
            });
            
             services.AddAuthorization(options =>
            {
                options.AddPolicy("write:envios", policy => policy.Requirements.Add(new HasScopeRequirement("write:envios", domain)));
                options.AddPolicy("read:envios", policy => policy.Requirements.Add(new HasScopeRequirement("read:envios", domain)));
                options.AddPolicy("write:novedades", policy => policy.Requirements.Add(new HasScopeRequirement("write:novedades", domain)));
                options.AddPolicy("write:operadores", policy => policy.Requirements.Add(new HasScopeRequirement("write:operadores", domain)));
                options.AddPolicy("read:operadores", policy => policy.Requirements.Add(new HasScopeRequirement("read:operadores", domain)));
                options.AddPolicy("delete:operadores", policy => policy.Requirements.Add(new HasScopeRequirement("delete:operadores", domain)));

            });

            /*
            services.AddDbContext<ApiContext>(opt => // Agregar
                                               opt.UseInMemoryDatabase("TodoList"));
                                               */
            services.AddControllers();

            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProcesadorEnviosAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcesadorEnviosAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
