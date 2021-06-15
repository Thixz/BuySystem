using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Request.OrderItems;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Services.OrderItems;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Services
{
    [TestClass]
    public class OrderItemServiceMockTest
    {
        [TestMethod]
        public async Task OrderItem_Post_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPostRequest _postRequest = new OrderItemPostRequest
            {
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 10,
                unityValue = 5,
                quantity = 5,
                total = 35
            };

            OrderItemEntity _orderItemEntity = new OrderItemEntity(_postRequest)
            {
                creationDate = DateTime.Now
            };

            mockOrderItemRepository
                .Setup(x => x.Create(It.IsAny<OrderItemEntity>()))
                .Returns(Task.FromResult(_orderItemEntity));

            var result = await mockOrderItemService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task OrderItem_Post_EmptyGuid()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPostRequest _postRequest = new OrderItemPostRequest
            {
                orderId = Guid.Empty,
                productId = Guid.Empty,
                freight = 10,
                unityValue = 5,
                quantity = 5,
                total = 35
            };

            mockOrderItemRepository
                .Setup(x => x.Create(It.IsAny<OrderItemEntity>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            var result = await mockOrderItemService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Post_Error()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPostRequest _postRequest = new OrderItemPostRequest
            {
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 0,
                unityValue = 0,
                quantity = 0,
                total = 0
            };

            mockOrderItemRepository
                .Setup(x => x.Create(It.IsAny<OrderItemEntity>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            var result = await mockOrderItemService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Post_Database_Error()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPostRequest _postRequest = new OrderItemPostRequest
            {
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 10,
                unityValue = 5,
                quantity = 5,
                total = 35
            };

            mockOrderItemRepository
                .Setup(x => x.Create(It.IsAny<OrderItemEntity>()))
                .Returns(Task.FromResult(new OrderItemEntity{Id = Guid.Empty }));

            var result = await mockOrderItemService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task OrderItem_Put_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPutRequest _putRequest = new OrderItemPutRequest
            {
                id = Guid.NewGuid(),
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 10,
                unityValue = 5,
                quantity = 5,
                total = 35
            };

            OrderItemEntity _orderItemEntity = new OrderItemEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-10"),
                updatedDate = DateTime.Now
            };

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            var result = await mockOrderItemService.Object.Update(_putRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task OrderItem_Put_EmptyGuid()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            var result = await mockOrderItemService.Object.Update(new OrderItemPutRequest {id = Guid.Empty });
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Put_Error()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPutRequest _putRequest = new OrderItemPutRequest
            {
                id = Guid.NewGuid(),
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 0,
                unityValue = 0,
                quantity = 0,
                total = 0
            };

            var result = await mockOrderItemService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Put_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPutRequest _putRequest = new OrderItemPutRequest
            {
                id = Guid.NewGuid(),
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 10,
                unityValue = 5,
                quantity = 5,
                total = 35
            };

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderItemEntity));

            var result = await mockOrderItemService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Put_UpdateError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            OrderItemPutRequest _putRequest = new OrderItemPutRequest
            {
                id = Guid.NewGuid(),
                orderId = Guid.NewGuid(),
                productId = Guid.NewGuid(),
                freight = 10,
                unityValue = 5,
                quantity = 5,
                total = 35
            };

            OrderItemEntity _orderItemEntity = new OrderItemEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-10"),
                updatedDate = DateTime.Now
            };

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            mockOrderItemRepository
               .Setup(x => x.Update(It.IsAny<OrderItemEntity>()))
               .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.Empty }));

            var result = await mockOrderItemService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task OrderItem_Delete_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            mockOrderItemRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var result = await mockOrderItemService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task OrderItem_Delete_EmptyID()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            var result = await mockOrderItemService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Delete_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderItemEntity));

            var result = await mockOrderItemService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Delete_Database_Error()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            mockOrderItemRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));

            var result = await mockOrderItemService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task OrderItem_Get_Success()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            var result = await mockOrderItemService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task OrderItem_Get_EmptyID()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity { Id = Guid.NewGuid() }));

            var result = await mockOrderItemService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task OrderItem_Get_IdNotFound()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderItemRepository = new Mock<OrderItemRepository>(mockMySqlContext.Object);
            var mockItemLogger = new Mock<ILogger<OrderItemService>>();

            mockOrderItemRepository.CallBase = true;

            var mockOrderItemService = new Mock<OrderItemService>(mockOrderItemRepository.Object, mockItemLogger.Object);

            mockOrderItemRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderItemEntity));

            var result = await mockOrderItemService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
