using System;
using System.Application.Data.MySql;
using System.Application.Data.Repositories;
using System.Application.Data.Repositories.Costumers;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Orders;
using System.Application.Data.Repositories.Products;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuySytemTests
{
    public class BuySystemTests : Startup
    {
        protected CostumerRepository _costumerRepository;
        protected AdressRepository _adressRepository;
        protected ProductRepository _productRepository;
        protected OrderRepository _orderRepository;
        protected OrderItemRepository _orderItemRepository;
        protected DefaultResponse _defaultResponse;
        protected MySqlContext _context;


        public BuySystemTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<CostumerRepository>();
            services.AddTransient<AdressRepository>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<OrderRepository>();
            services.AddTransient<OrderItemRepository>();
            services.AddTransient<DefaultResponse>();
            services.AddScoped<MySqlContext>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            var serviceProvider = services.BuildServiceProvider();
            _costumerRepository = (CostumerRepository)serviceProvider.GetService(typeof(CostumerRepository));
            _adressRepository = (AdressRepository)serviceProvider.GetService(typeof(AdressRepository));
            _context = (MySqlContext)serviceProvider.GetService(typeof(MySqlContext));
            _productRepository = (ProductRepository)serviceProvider.GetService(typeof(ProductRepository));
            _orderRepository = (OrderRepository)serviceProvider.GetService(typeof(OrderRepository));
            _orderItemRepository = (OrderItemRepository)serviceProvider.GetService(typeof(OrderItemRepository));
            _defaultResponse = (DefaultResponse)serviceProvider.GetService(typeof(DefaultResponse));
        }
    }
}
