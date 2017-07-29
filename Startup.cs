using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace testdocsapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // To implement CORS, uncomment code below
            // https://docs.asp.net/en/latest/security/cors.html#enabling-cors-with-middleware
            // services.AddCors(options =>
            // {
            //     string[] sites = new string[]{ 
            //         "http://clientapp1.com", 
            //         "http://www.clientapp.com", 
            //         "http://localhost:4200"
            //         };
                
            //     options.AddPolicy("AllowSpecificOrigin",
            //             builder => builder.WithOrigins(sites));
            // });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(policy => policy
             .AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials()
            );

            var options = new JwtBearerOptions
            {
                Audience = "http://testdocs.azurewebsites.net/",
                Authority = "https://testdocs.auth0.com/"
            };

            app.UseJwtBearerAuthentication(options);

            app.UseMvc();
        }
    }
}
