using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Orders;
using System.Application.Services.OrderItems;
using System.Application.Services.Orders;
using System.Collections.Generic;
using System.Text;

namespace System.Order.API.Default
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
            services.AddScoped<OrderService>();
            services.AddScoped<OrderItemService>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<OrderItemRepository>();
            services.AddScoped<MySqlContext>();
            //services.Configure<>
            services.AddApiVersioning();
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
