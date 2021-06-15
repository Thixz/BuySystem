using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Contracts.Request.Costumers;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.Entities.Orders;
using System.Application.Data.MySql;
using System.Application.Data.Repositories;
using System.Application.Data.Repositories.Costumers;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Orders;
using System.Application.Services.Adresses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CostumerTests.Services
{
    [TestClass]
    public class AddressServiceMockTest
    {
        [TestMethod]
        public async Task Address_Post_Success() 
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderRepository = new Mock<OrderRepository>(mockMySqlContext.Object);
            var mockAddressRepository = new Mock<AdressRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<AdressService>>();
            var mockCostumerRepository = new Mock<CostumerRepository>(mockMySqlContext.Object);

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPostRequest _postRequest = new AdressPostRequest
            {
                costumerId = Guid.NewGuid(),
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"  
            };

            AdressEntity _addressResponse = new AdressEntity(_postRequest)
            {
                creationDate = DateTime.Now,
            };

            mockAddressRepository
             .Setup(x => x.Create(It.IsAny<AdressEntity>()))
             .Returns(Task.FromResult(_addressResponse));

            var response = await mockAddressService.Object.Create(_postRequest);
            Assert.IsTrue(response.success);
        }
        [TestMethod]
        public async Task Address_Post_EmptyCostumerID()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPostRequest _postRequest = new AdressPostRequest
            {
                costumerId = Guid.Empty,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };

            var result = await mockAddressService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Post_Error()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPostRequest _postRequest = new AdressPostRequest
            {
                costumerId = Guid.NewGuid(),
                address = "",
                addressNumber = "",
                neighborhood = "",
                postalCode = "",
                country = ""
            };

            var result = await mockAddressService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Post_CreateError()
        {
            var mockMySqlContext = new Mock<MySqlContext>();
            var mockOrderRepository = new Mock<OrderRepository>(mockMySqlContext.Object);
            var mockAddressRepository = new Mock<AdressRepository>(mockMySqlContext.Object);
            var mocklogger = new Mock<ILogger<AdressService>>();
            var mockCostumerRepository = new Mock<CostumerRepository>(mockMySqlContext.Object);

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPostRequest _postRequest = new AdressPostRequest
            {
                costumerId = Guid.NewGuid(),
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };

            AdressEntity _addressResponse = new AdressEntity(_postRequest)
            {
                creationDate = DateTime.Now,
            };

            mockAddressRepository
             .Setup(x => x.Create(It.IsAny<AdressEntity>()))
             .Returns(Task.FromResult(new AdressEntity() {Id = Guid.Empty }));

            var response = await mockAddressService.Object.Create(_postRequest);
            Assert.IsTrue(!response.success);
        }


        [TestMethod]
        public async Task Address_Put_Success()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPutRequest _putRequest = new AdressPutRequest
            {
                Id = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };

            AdressEntity _adressEntity = new AdressEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-20"),
                updatedDate = DateTime.Now
            };

            mockAddressRepository
             .Setup(x => x.Update(_adressEntity))
             .Returns(Task.FromResult(_adressEntity));

            var result = await mockAddressService.Object.Update(_putRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Address_Put_EmptyID()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPutRequest _putRequest = new AdressPutRequest
            {
                Id = Guid.Empty,
                costumerId = Guid.NewGuid(),
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };

            var result = await mockAddressService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Put_EmptyCostumerID()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPutRequest _putRequest = new AdressPutRequest
            {
                Id = Guid.NewGuid(),
                costumerId = Guid.Empty,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };

            var result = await mockAddressService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Put_Error()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPutRequest _putRequest = new AdressPutRequest
            {
                Id = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                address = "",
                addressNumber = "",
                neighborhood = "",
                postalCode = "",
                country = ""
            };

            var result = await mockAddressService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Put_UpdateError() // A forma que encontrei de executar o código no repositório e retornar o erro que queria foi esta....
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            AdressPutRequest _putRequest = new AdressPutRequest
            {
                Id = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };

            await mockAddressRepository.Object.Update(It.IsAny<AdressEntity>());

            mockAddressRepository
             .Setup(x => x.Update(It.IsAny<AdressEntity>()))
             .Returns(Task.FromResult(new AdressEntity() { Id = Guid.Empty }));

            var result = await mockAddressService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Address_Delete_Success()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            mockOrderRepository
            .Setup(x => x.GetOrderByAddressId(It.IsAny<Guid>()))
            .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
             .Setup(x => x.Get(It.IsAny<Guid>()))
             .Returns(Task.FromResult(new AdressEntity { Id = Guid.NewGuid() }));

            mockAddressRepository
             .Setup(x => x.Delete(It.IsAny<Guid>()))
             .Returns(Task.FromResult(true));

            var result = await mockAddressService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Address_Delete_EmptyID()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            var result = await mockAddressService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Delete_AciveOrder()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            mockOrderRepository
            .Setup(x => x.GetOrderByAddressId(It.IsAny<Guid>()))
            .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid()}));

            var result = await mockAddressService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Delete_IdNotFound()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            mockOrderRepository
            .Setup(x => x.GetOrderByAddressId(It.IsAny<Guid>()))
            .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
             .Setup(x => x.Get(It.IsAny<Guid>()))
             .Returns(Task.FromResult(null as AdressEntity));

            var result = await mockAddressService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Delete_Database_Error()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            mockOrderRepository
            .Setup(x => x.GetOrderByAddressId(It.IsAny<Guid>()))
            .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
             .Setup(x => x.Get(It.IsAny<Guid>()))
             .Returns(Task.FromResult(new AdressEntity { Id = Guid.NewGuid() }));

            mockAddressRepository
             .Setup(x => x.Delete(It.IsAny<Guid>()))
             .Returns(Task.FromResult(false));

            var result = await mockAddressService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Address_Get_Success()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            mockAddressRepository
             .Setup(x => x.Get(It.IsAny<Guid>()))
             .Returns(Task.FromResult(new AdressEntity { Id = Guid.NewGuid() }));

            var result = await mockAddressService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Address_Get_EmptyID()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            var result = await mockAddressService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Address_Get_IdNotFound()
        {
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<AdressService>>();

            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockAddressService = new Mock<AdressService>(mockAddressRepository.Object, mocklogger.Object, mockOrderRepository.Object);

            mockAddressRepository
             .Setup(x => x.Get(It.IsAny<Guid>()))
             .Returns(Task.FromResult(null as AdressEntity));

            var result = await mockAddressService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
