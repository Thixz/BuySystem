using System;
using System.Application.Data.MySql;
using System.Application.Data.Repositories;
using System.Application.Data.Repositories.Costumers;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuySytemTests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup()
        {

        }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            BeforeConfigureServices(services);
            services.AddScoped<MySqlContext>();
            services.AddScoped<AdressRepository>();
            services.AddScoped<CostumerRepository>();
            services.AddScoped<MySqlContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void BeforeConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
