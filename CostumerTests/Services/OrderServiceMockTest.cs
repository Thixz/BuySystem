using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Request.Orders;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Orders;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Orders;
using System.Application.Services.OrderItems;
using System.Application.Services.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Services
{
    [TestClass]
    public class OrderServiceMockTest
    {
        [TestMethod]
        public async Task Order_Post_Success()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            OrderPostRequest _postRequest = new OrderPostRequest
            {
                costumerAddressId = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                freight = 15,
                items = new List<OrderItemEntity> 
                {
                    new OrderItemEntity
                    {
                        Id = Guid.NewGuid(),
                        orderId = Guid.NewGuid(),
                        productId = Guid.NewGuid(),
                        freight = 15,
                        unityValue = 30,
                        total = 45,
                        quantity = 1,
                    }
                },
                orderNumber = "15001",
                subTotal = 30,
                total = 45
            };

            OrderEntity _orderEntity = new OrderEntity(_postRequest)
            {
                creationDate = DateTime.Now
            };

            mockOrderRepository
                .Setup(x => x.GetOrderByNumber(It.IsAny<string>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockOrderRepository
                .Setup(x => x.Create(It.IsAny<OrderEntity>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid() ,items = _postRequest.items}));

            mockOrderItemRepository
                .Setup(x => x.Create(It.IsAny<OrderItemEntity>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            var result = await mockOrderService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Order_Post_Fail()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            OrderPostRequest _postRequest = new OrderPostRequest
            {
                costumerAddressId = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                freight = 0,
                items = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = Guid.NewGuid(),
                        orderId = Guid.NewGuid(),
                        productId = Guid.NewGuid(),
                        freight = 15,
                        unityValue = 30,
                        total = 45,
                        quantity = 1,
                    }
                },
                orderNumber = "",
                subTotal = 0,
                total = 0
            };

            var result = await mockOrderService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Post_WrongValue()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            OrderPostRequest _postRequest = new OrderPostRequest
            {
                costumerAddressId = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                freight = 15,
                items = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = Guid.NewGuid(),
                        orderId = Guid.NewGuid(),
                        productId = Guid.NewGuid(),
                        freight = 5,
                        unityValue = 0,
                        total = 5,
                        quantity = 1,
                    }
                },
                orderNumber = "15001",
                subTotal = 30,
                total = 45
            };

            var result = await mockOrderService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Post_Duplicated_OrderNumber()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            OrderPostRequest _postRequest = new OrderPostRequest
            {
                costumerAddressId = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                freight = 15,
                items = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = Guid.NewGuid(),
                        orderId = Guid.NewGuid(),
                        productId = Guid.NewGuid(),
                        freight = 15,
                        unityValue = 30,
                        total = 45,
                        quantity = 1,
                    }
                },
                orderNumber = "15001",
                subTotal = 30,
                total = 45
            };

            mockOrderRepository
                .Setup(x => x.GetOrderByNumber(It.IsAny<string>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid()}));

            var result = await mockOrderService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Post_Database_Error()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            OrderPostRequest _postRequest = new OrderPostRequest
            {
                costumerAddressId = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                freight = 15,
                items = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = Guid.NewGuid(),
                        orderId = Guid.NewGuid(),
                        productId = Guid.NewGuid(),
                        freight = 15,
                        unityValue = 30,
                        total = 45,
                        quantity = 1,
                    }
                },
                orderNumber = "15001",
                subTotal = 30,
                total = 45
            };

            mockOrderRepository
                .Setup(x => x.GetOrderByNumber(It.IsAny<string>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockOrderRepository
                .Setup(x => x.Create(It.IsAny<OrderEntity>()))
                .Returns(Task.FromResult(new OrderEntity {Id = Guid.Empty }));

            var result = await mockOrderService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Post_OrderItem_Database_Error()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            OrderPostRequest _postRequest = new OrderPostRequest
            {
                costumerAddressId = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                freight = 15,
                items = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = Guid.NewGuid(),
                        orderId = Guid.NewGuid(),
                        productId = Guid.NewGuid(),
                        freight = 15,
                        unityValue = 30,
                        total = 45,
                        quantity = 1,
                    }
                },
                orderNumber = "15001",
                subTotal = 30,
                total = 45
            };

            mockOrderRepository
                .Setup(x => x.GetOrderByNumber(It.IsAny<string>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockOrderRepository
                .Setup(x => x.Create(It.IsAny<OrderEntity>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid(), items = _postRequest.items }));

            mockOrderItemRepository
                .Setup(x => x.Create(It.IsAny<OrderItemEntity>()))
                .Returns(Task.FromResult(new OrderItemEntity {Id = Guid.Empty }));

            var result = await mockOrderService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }



        [TestMethod]
        public async Task Order_Delete_Success()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as List<OrderItemEntity>));

            mockOrderRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid()}));

            mockOrderRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var result = await mockOrderService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Order_Delete_EmptyID()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            var result = await mockOrderService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Delete_ActiveOrderItem()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new List<OrderItemEntity>()));

            var result = await mockOrderService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Delete_IdNotFound()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as List<OrderItemEntity>));

            mockOrderRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderEntity));

            var result = await mockOrderService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Delete_Database_Error()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as List<OrderItemEntity>));

            mockOrderRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid() }));

            mockOrderRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var result = await mockOrderService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Order_Get_Success()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            var _orderItemEntity = new OrderItemEntity()
            {
                Id = Guid.NewGuid(),
                freight = 10,
                productId = Guid.NewGuid(),
                orderId = Guid.NewGuid(),
                quantity = 5,
                unityValue = 50,
                total = 260     
            };

            var _list = new List<OrderItemEntity>();
            _list.Add(_orderItemEntity);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as List<OrderItemEntity>));

            mockOrderRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid() }));

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(_list));

            var result = await mockOrderService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Order_Get_EmptyId()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            var result = await mockOrderService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Order_Get_IdNotFound()
        {
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<OrderService>>();
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();
            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockOrderService = new Mock<OrderService>(mockOrderRepository.Object, mockOrderItemService.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByOrderId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as List<OrderItemEntity>));

            mockOrderRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderEntity));

            var result = await mockOrderService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
