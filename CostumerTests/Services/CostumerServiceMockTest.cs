using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.Entities.Orders;
using System.Application.Data.MySql;
using System.Application.Data.Repositories;
using System.Application.Data.Repositories.Costumers;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Orders;
using System.Application.Services.Costumers;
using System.Threading.Tasks;

namespace CostumerTests.Services
{
    [TestClass]
    public class CostumerServiceMockTest
    {
        [TestMethod]
        public async Task Costumer_Post_Success()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPostRequest _postRequest = new CostumerPostRequest
            {
                costumerName = "Thiago",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            CostumerEntity _costumerEntity = new CostumerEntity(_postRequest)
            {
                creationDate = DateTime.Now
            };

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as CostumerEntity));

            mockCostumerRepository
                .Setup(x => x.Create(It.IsAny<CostumerEntity>()))
                .Returns(Task.FromResult(_costumerEntity));

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Costumer_Post_Fail()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPostRequest _postRequest = new CostumerPostRequest
            {
                costumerName = "",
                email = "",
                document = "",
                phoneNumber = "",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as CostumerEntity));

            mockCostumerRepository
                .Setup(x => x.Create(It.IsAny<CostumerEntity>()))
                .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Post_Duplicated_Document()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPostRequest _postRequest = new CostumerPostRequest
            {
                costumerName = "Thiago",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Post_Database_Error()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPostRequest _postRequest = new CostumerPostRequest
            {
                costumerName = "Thiago",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as CostumerEntity));

            mockCostumerRepository
                .Setup(x => x.Create(It.IsAny<CostumerEntity>()))
                .Returns(Task.FromResult(new CostumerEntity {Id = Guid.Empty }));;

            var result = await mockCostumerService.Object.Create(_postRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Costumer_Put_Success()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPutRequest _putRequest = new CostumerPutRequest
            {
                id = Guid.NewGuid(),
                costumerName = "Thiago II",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            CostumerEntity _costumerEntity = new CostumerEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-10"),
                updatedDate = DateTime.Now
            };

            mockCostumerRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as CostumerEntity));

            mockCostumerRepository
                .Setup(x => x.Update(It.IsAny<CostumerEntity>()))
                .Returns(Task.FromResult(_costumerEntity));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Costumer_Put_EmptyID()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPutRequest _putRequest = new CostumerPutRequest
            {
                id = Guid.Empty,
                costumerName = "Thiago II",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Put_Error()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPutRequest _putRequest = new CostumerPutRequest
            {
                id = Guid.NewGuid(),
                costumerName = "",
                email = "",
                document = "",
                phoneNumber = "",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Put_IdNotFound()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPutRequest _putRequest = new CostumerPutRequest
            {
                id = Guid.NewGuid(),
                costumerName = "Thiago II",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            mockCostumerRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as CostumerEntity));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Put_Duplicated_Document()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPutRequest _putRequest = new CostumerPutRequest
            {
                id = Guid.NewGuid(),
                costumerName = "Thiago II",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            mockCostumerRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Put_UpdateError()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            CostumerPutRequest _putRequest = new CostumerPutRequest
            {
                id = Guid.NewGuid(),
                costumerName = "Thiago II",
                email = "thiago@teste",
                document = "156516516",
                phoneNumber = "989595959",
                birthdate = DateTime.Parse("1994-12-09"),
                gender = 1
            };

            CostumerEntity _costumerEntity = new CostumerEntity(_putRequest)
            {
                creationDate = DateTime.Parse("2020-05-10"),
                updatedDate = DateTime.Now
            };

            mockCostumerRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            mockCostumerRepository
                .Setup(x => x.GetByDocument(It.IsAny<string>()))
                .Returns(Task.FromResult(null as CostumerEntity));

            mockCostumerRepository
                .Setup(x => x.Update(It.IsAny<CostumerEntity>()))
                .Returns(Task.FromResult(new CostumerEntity() { Id = Guid.Empty })); ;

            var result = await mockCostumerService.Object.Update(_putRequest);
            Assert.IsTrue(!result.success);
        }


        [TestMethod]
        public async Task Costumer_Delete_Success()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);


            mockOrderRepository
                .Setup(x => x.GetOrderByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
                .Setup(x => x.GetAddressByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as AdressEntity));

            mockCostumerRepository
               .Setup(x => x.Get(It.IsAny<Guid>()))
               .Returns(Task.FromResult(new CostumerEntity{Id = Guid.NewGuid()}));

            mockCostumerRepository
               .Setup(x => x.Delete(It.IsAny<Guid>()))
               .Returns(Task.FromResult(true));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Costumer_Delete_EmptyID()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            var result = await mockCostumerService.Object.Delete(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Delete_ActiveOrder()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);


            mockOrderRepository
                .Setup(x => x.GetOrderByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new OrderEntity { Id = Guid.NewGuid()}));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Delete_ActiveAddress()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);


            mockOrderRepository
                .Setup(x => x.GetOrderByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
                .Setup(x => x.GetAddressByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new AdressEntity { Id = Guid.NewGuid()}));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Delete_IdNotFound()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);


            mockOrderRepository
                .Setup(x => x.GetOrderByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
                .Setup(x => x.GetAddressByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as AdressEntity));

            mockCostumerRepository
               .Setup(x => x.Get(It.IsAny<Guid>()))
               .Returns(Task.FromResult(null as CostumerEntity));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Delete_Database_Error()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);


            mockOrderRepository
                .Setup(x => x.GetOrderByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as OrderEntity));

            mockAddressRepository
                .Setup(x => x.GetAddressByCostumerId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(null as AdressEntity));

            mockCostumerRepository
               .Setup(x => x.Get(It.IsAny<Guid>()))
               .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            mockCostumerRepository
               .Setup(x => x.Delete(It.IsAny<Guid>()))
               .Returns(Task.FromResult(false));

            var result = await mockCostumerService.Object.Delete(Guid.NewGuid());
            Assert.IsTrue(!result.success); 
        }


        [TestMethod]
        public async Task Costumer_Get_Success()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            mockCostumerRepository
               .Setup(x => x.Get(It.IsAny<Guid>()))
               .Returns(Task.FromResult(new CostumerEntity { Id = Guid.NewGuid() }));

            var result = await mockCostumerService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(result.success);
        }
        [TestMethod]
        public async Task Costumer_Get_EmptyID()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            var result = await mockCostumerService.Object.Get(Guid.Empty);
            Assert.IsTrue(!result.success);
        }
        [TestMethod]
        public async Task Costumer_Get_IdNotFound()
        {
            var mockCostumerRepository = new Mock<CostumerRepository>(new MySqlContext());
            var mockOrderRepository = new Mock<OrderRepository>(new MySqlContext());
            var mockAddressRepository = new Mock<AdressRepository>(new MySqlContext());
            var mocklogger = new Mock<ILogger<CostumerService>>();

            mockCostumerRepository.CallBase = true;
            mockOrderRepository.CallBase = true;
            mockAddressRepository.CallBase = true;

            var mockCostumerService = new Mock<CostumerService>(mockCostumerRepository.Object, mocklogger.Object, mockOrderRepository.Object, mockAddressRepository.Object);

            mockCostumerRepository
               .Setup(x => x.Get(It.IsAny<Guid>()))
               .Returns(Task.FromResult(null as CostumerEntity));

            var result = await mockCostumerService.Object.Get(Guid.NewGuid());
            Assert.IsTrue(!result.success);
        }
    }
}
