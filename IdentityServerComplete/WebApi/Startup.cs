using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi
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
            services
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //services.AddAuthentication()
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => { options.Authority = "http://localhost:5000"; options.RequireHttpsMetadata = false; options.Audience = "api1"; options.SaveToken = true; });


            //test
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies";
            //    options.DefaultChallengeScheme = "oidc";

            //})
            //     .AddCookie("Cookies")
            //     .AddOpenIdConnect("oidc", options =>
            //     {
            //         options.Authority = "http://localhost:5000";
            //         options.RequireHttpsMetadata = false;

            //         options.ClientId = "api";
            //         options.ClientSecret = "secret";
            //         options.ResponseType = "code id_token";
            //         options.SaveTokens = true;

            //         // options.Scope.Add("api1");
            //         options.Scope.Add("offline_access");

            //     });
            //jwt
            //services.AddAuthentication()
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => { options.Authority = "http://localhost:5000"; options.RequireHttpsMetadata = false; options.Audience = "api1"; options.SaveToken = true; });

            //end

                    services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.Authority = "http://localhost:5000";
                   options.RequireHttpsMetadata = false;
                   options.ApiName = "api1";
                   options.SaveToken = true;
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
