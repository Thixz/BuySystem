using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Repositories
{
    [TestClass]
    public class OrderRepositoryTest : BuySystemTests
    {
        [TestInitialize]
        public void Initiliaze()
        {
            var connection = _context.Connect();
            connection.ExecuteAsync("delete from buysystem.orderitems");
            connection.ExecuteAsync("delete from buysystem.orders");
            connection.ExecuteAsync("delete from buysystem.addresses");
            connection.ExecuteAsync("delete from buysystem.costumers");
        }

        [TestMethod]
        public async Task Order_Repository_Post_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity 
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            });

            var _address = await _adressRepository.Create(new AdressEntity 
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            }

            );
            var _orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight =10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            };

            var response = await _orderRepository.Create(_orderEntity);

            Assert.IsNotNull(response);

            Assert.AreEqual(_orderEntity.Id, response.Id);
            Assert.AreEqual(_orderEntity.costumerId, response.costumerId);
            Assert.AreEqual(_orderEntity.costumerAddressId, response.costumerAddressId);
            Assert.AreEqual(_orderEntity.freight, response.freight);
            Assert.AreEqual(_orderEntity.orderNumber, response.orderNumber);
            Assert.AreEqual(_orderEntity.total, response.total);
            Assert.AreEqual(_orderEntity.subTotal, response.subTotal);
        }
        [TestMethod]
        public async Task Order_Repository_Post_Error()
        {
            var response = await _orderRepository.Create(null as OrderEntity);

            Assert.AreEqual(response.Id, Guid.Empty) ;
        }
        [TestMethod]
        public async Task Order_Repository_Delete_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            });

            var _address = await _adressRepository.Create(new AdressEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            }

            );
            var _orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            };

            var response = await _orderRepository.Create(_orderEntity);
            var deleted = await _orderRepository.Delete(response.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task Order_Repository_Get_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            });

            var _address = await _adressRepository.Create(new AdressEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            }

            );
            var _orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            };

            var response = await _orderRepository.Create(_orderEntity);
            var get = await _orderRepository.Get(response.Id);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.costumerId, response.costumerId);
            Assert.AreEqual(get.costumerAddressId, response.costumerAddressId);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.orderNumber, response.orderNumber);
            Assert.AreEqual(get.total, response.total);
            Assert.AreEqual(get.subTotal, response.subTotal);
        }
        [TestMethod]
        public async Task Order_Repository_GetOrderByNumber_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            });

            var _address = await _adressRepository.Create(new AdressEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            }

            );
            var _orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            };

            var response = await _orderRepository.Create(_orderEntity);
            var get = await _orderRepository.GetOrderByNumber(response.orderNumber);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.costumerId, response.costumerId);
            Assert.AreEqual(get.costumerAddressId, response.costumerAddressId);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.orderNumber, response.orderNumber);
            Assert.AreEqual(get.total, response.total);
            Assert.AreEqual(get.subTotal, response.subTotal);
        }
        [TestMethod]
        public async Task Order_Repository_GetOrderByCostumerId_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            });

            var _address = await _adressRepository.Create(new AdressEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            }

            );
            var _orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            };

            var response = await _orderRepository.Create(_orderEntity);
            var get = await _orderRepository.GetOrderByCostumerId(response.costumerId);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.costumerId, response.costumerId);
            Assert.AreEqual(get.costumerAddressId, response.costumerAddressId);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.orderNumber, response.orderNumber);
            Assert.AreEqual(get.total, response.total);
            Assert.AreEqual(get.subTotal, response.subTotal);
        }
        [TestMethod]
        public async Task Order_Repository_GetOrderByAddressId_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            });

            var _address = await _adressRepository.Create(new AdressEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            }

            );
            var _orderEntity = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            };

            var response = await _orderRepository.Create(_orderEntity);
            var get = await _orderRepository.GetOrderByAddressId(response.costumerAddressId);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.costumerId, response.costumerId);
            Assert.AreEqual(get.costumerAddressId, response.costumerAddressId);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.orderNumber, response.orderNumber);
            Assert.AreEqual(get.total, response.total);
            Assert.AreEqual(get.subTotal, response.subTotal);
        }

    }
}
