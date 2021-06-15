using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Request.Products;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Products;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Products;
using System.Application.Services.Products;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Services
{
    [TestClass]
    public class ProductServiceMockTest
    {
        [TestMethod]
        public async Task Product_Post_Success()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPostRequest _postRequest = new ProductPostRequest
            {
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            ProductEntity _productEntity = new ProductEntity(_postRequest)
            {
                creationDate = DateTime.Now
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(null as ProductEntity));

            mockProductRepository
                .Setup(x => x.Create(It.IsAny<ProductEntity>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Product_Post_Error()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPostRequest _postRequest = new ProductPostRequest
            {
                productName = "",
                model = "",
                productCode = "",
                productValue = 0,
                productDescription = "",
                quantity = 0
            };

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Post_Duplicated_Document()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPostRequest _postRequest = new ProductPostRequest
            {
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid()}));

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Post_Database_Error()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPostRequest _postRequest = new ProductPostRequest
            {
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(null as ProductEntity));

            mockProductRepository
                .Setup(x => x.Create(It.IsAny<ProductEntity>()))
                .Returns(Task.FromResult(new ProductEntity() {Id = Guid.Empty }));

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }



        [TestMethod]
        public async Task Product_Put_Success()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPutRequest _putRequest = new ProductPutRequest
            {
                id = Guid.NewGuid(),
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            ProductEntity _productEntity = new ProductEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-10"),
                updatedDate = DateTime.Now
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(null as ProductEntity));

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Product_Put_EmptyID()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPutRequest _putRequest = new ProductPutRequest
            {
                id = Guid.Empty,
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Put_Error()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPutRequest _putRequest = new ProductPutRequest
            {
                id = Guid.NewGuid(),
                productName = "",
                model = "",
                productCode = "",
                productValue = 0,
                productDescription = "",
                quantity = 0
            };

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Put_Duplicated_ProductCode()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPutRequest _putRequest = new ProductPutRequest
            {
                id = Guid.NewGuid(),
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid()}));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Put_IdNotFound()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPutRequest _putRequest = new ProductPutRequest
            {
                id = Guid.NewGuid(),
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(null as ProductEntity));

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ProductEntity));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Put_UpdateError()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            ProductPutRequest _putRequest = new ProductPutRequest
            {
                id = Guid.NewGuid(),
                productName = "Teste",
                model = "Teste",
                productCode = "516516",
                productValue = 100.50,
                productDescription = "Teste",
                quantity = 10
            };

            ProductEntity _productEntity = new ProductEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-10"),
                updatedDate = DateTime.Now
            };

            mockProductRepository
                .Setup(x => x.GetByCode(It.IsAny<string>()))
                .Returns(Task.FromResult(null as ProductEntity));

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid() }));

            mockProductRepository
                .Setup(x => x.Update(It.IsAny<ProductEntity>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.Empty }));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Product_Delete_Success()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByProductId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderItemEntity));

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid() }));

            mockProductRepository
               .Setup(x => x.Delete(It.IsAny<Guid>()))
               .Returns(Task.FromResult(true));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Product_Delete_EmptyID()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            var result = await mockCostumerService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Delete_ActiveOrderItem()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByProductId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderItemEntity {Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Delete_IdNotFound()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByProductId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderItemEntity));

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ProductEntity));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Delete_Database_Error()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockOrderItemRepository
                .Setup(x => x.GetOrderItemByProductId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderItemEntity));

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid() }));

            mockProductRepository
               .Setup(x => x.Delete(It.IsAny<Guid>()))
               .Returns(Task.FromResult(false));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Product_Get_Success()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new ProductEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Product_Get_EmptyID()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            var result = await mockCostumerService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Product_Get_IdNotFound()
        {
            var mockProductRepository = new Mock<ProductRepository>(new MySqlContext());
            var mockOrderItemRepository = new Mock<OrderItemRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<ProductService>>();

            mockProductRepository.CallBase = true;
            mockOrderItemRepository.CallBase = true;

            var mockCostumerService = new Mock<ProductService>(mockProductRepository.Object, mocklogger.Object, mockOrderItemRepository.Object);

            mockProductRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as ProductEntity));

            var result = await mockCostumerService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
