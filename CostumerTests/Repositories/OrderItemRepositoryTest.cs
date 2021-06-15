using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Orders;
using System.Application.Data.Entities.Products;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Repositories
{
    [TestClass]
   public class OrderItemRepositoryTest : BuySystemTests
    {
        [TestInitialize]
        public void Initiliaze()
        {
            var connection = _context.Connect();
            connection.ExecuteAsync("delete from buysystem.orderitems");
            connection.ExecuteAsync("delete from buysystem.orders");
            connection.ExecuteAsync("delete from buysystem.addresses");
            connection.ExecuteAsync("delete from buysystem.costumers");
            connection.ExecuteAsync("delete from buysystem.products");
        }

        [TestMethod]
        public async Task OrderItem_Repository_Post_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
               costumerName = "Teste",
               document = "5151544",
               email = "teste@teste",
               gender = 2,
               birthdate = DateTime.Parse("2021-05-28"),
               phoneNumber = "54145545"
            });

            var _product = await _productRepository.Create(new ProductEntity
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
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
            });

            
            var _order = await _orderRepository.Create(new OrderEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            });

            var _orderItemEntity = new OrderItemEntity()
            {
                Id = Guid.NewGuid(),
                productId = _product.Id,
                orderId = _order.Id,
                quantity = 1,
                freight = 10,
                total = 110,
                unityValue = 100,
            };

            var response = await _orderItemRepository.Create(_orderItemEntity);

            Assert.IsNotNull(response);

            Assert.AreEqual(_orderItemEntity.Id, response.Id);
            Assert.AreEqual(_orderItemEntity.productId, response.productId);
            Assert.AreEqual(_orderItemEntity.orderId, response.orderId);
            Assert.AreEqual(_orderItemEntity.freight, response.freight);
            Assert.AreEqual(_orderItemEntity.quantity, response.quantity);
            Assert.AreEqual(_orderItemEntity.freight, response.freight);
            Assert.AreEqual(_orderItemEntity.total, response.total);
            Assert.AreEqual(_orderItemEntity.unityValue, response.unityValue);
        }
        [TestMethod]
        public async Task OrderItem_Repository_Post_Error()
        {
            var response = await _orderItemRepository.Create(null as OrderItemEntity);

            Assert.AreEqual(response.Id,Guid.Empty);
        }
        [TestMethod]
        public async Task OrderItem_Repository_Put_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "Teste",
                document = "5151544",
                email = "teste@teste",
                gender = 2,
                birthdate = DateTime.Parse("2021-05-28"),
                phoneNumber = "54145545"
            });

            var _product = await _productRepository.Create(new ProductEntity
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
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
            });


            var _order = await _orderRepository.Create(new OrderEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            });

            var _orderItemEntity = new OrderItemEntity
            {
                Id = Guid.NewGuid(),
                productId = _product.Id,
                orderId = _order.Id,
                quantity = 1,
                freight = 10,
                total = 110,
                unityValue = 100,
            };

            var response = await _orderItemRepository.Create(_orderItemEntity);
            var updated = await _orderItemRepository.Update(response);

            Assert.IsNotNull(updated);

            Assert.AreEqual(updated.Id, response.Id);
            Assert.AreEqual(updated.productId, response.productId);
            Assert.AreEqual(updated.orderId, response.orderId);
            Assert.AreEqual(updated.freight, response.freight);
            Assert.AreEqual(updated.quantity, response.quantity);
            Assert.AreEqual(updated.freight, response.freight);
            Assert.AreEqual(updated.total, response.total);
            Assert.AreEqual(updated.unityValue, response.unityValue);
        }
        [TestMethod]
        public async Task OrderItem_Repository_Put_Error()
        {
            var updated = await _orderItemRepository.Update(null as OrderItemEntity);

            Assert.AreEqual(updated.Id , Guid.Empty);
        }
        [TestMethod]
        public async Task OrderItem_Repository_Delete_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "Teste",
                document = "5151544",
                email = "teste@teste",
                gender = 2,
                birthdate = DateTime.Parse("2021-05-28"),
                phoneNumber = "54145545"
            });

            var _product = await _productRepository.Create(new ProductEntity
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
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
            });


            var _order = await _orderRepository.Create(new OrderEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            });

            var _orderItemEntity = new OrderItemEntity
            {
                Id = Guid.NewGuid(),
                productId = _product.Id,
                orderId = _order.Id,
                quantity = 1,
                freight = 10,
                total = 110,
                unityValue = 100,
            };

            var response = await _orderItemRepository.Create(_orderItemEntity);
            var deleted = await _orderItemRepository.Delete(response.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task OrderItem_Repository_Get_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "Teste",
                document = "5151544",
                email = "teste@teste",
                gender = 2,
                birthdate = DateTime.Parse("2021-05-28"),
                phoneNumber = "54145545"
            });

            var _product = await _productRepository.Create(new ProductEntity
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
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
            });


            var _order = await _orderRepository.Create(new OrderEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            });

            var _orderItemEntity = new OrderItemEntity
            {
                Id = Guid.NewGuid(),
                productId = _product.Id,
                orderId = _order.Id,
                quantity = 1,
                freight = 10,
                total = 110,
                unityValue = 100,
            };

            var response = await _orderItemRepository.Create(_orderItemEntity);
            var get = await _orderItemRepository.Get(response.Id);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.productId, response.productId);
            Assert.AreEqual(get.orderId, response.orderId);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.quantity, response.quantity);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.total, response.total);
            Assert.AreEqual(get.unityValue, response.unityValue);
        }
        [TestMethod]
        public async Task OrderItem_Repository_GetOrderItemByProductId_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "Teste",
                document = "5151544",
                email = "teste@teste",
                gender = 2,
                birthdate = DateTime.Parse("2021-05-28"),
                phoneNumber = "54145545"
            });

            var _product = await _productRepository.Create(new ProductEntity
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
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
            });


            var _order = await _orderRepository.Create(new OrderEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            });

            var _orderItemEntity = new OrderItemEntity
            {
                Id = Guid.NewGuid(),
                productId = _product.Id,
                orderId = _order.Id,
                quantity = 1,
                freight = 10,
                total = 110,
                unityValue = 100,
            };

            var response = await _orderItemRepository.Create(_orderItemEntity);
            var get = await _orderItemRepository.GetOrderItemByProductId(response.productId);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.productId, response.productId);
            Assert.AreEqual(get.orderId, response.orderId);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.quantity, response.quantity);
            Assert.AreEqual(get.freight, response.freight);
            Assert.AreEqual(get.total, response.total);
            Assert.AreEqual(get.unityValue, response.unityValue);
        }
        [TestMethod]
        public async Task OrderItem_Repository_GetOrderItemByOrderId_Success()
        {
            var _costumer = await _costumerRepository.Create(new CostumerEntity
            {
                Id = Guid.NewGuid(),
                costumerName = "Teste",
                document = "5151544",
                email = "teste@teste",
                gender = 2,
                birthdate = DateTime.Parse("2021-05-28"),
                phoneNumber = "54145545"
            });

            var _product = await _productRepository.Create(new ProductEntity
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
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
            });


            var _order = await _orderRepository.Create(new OrderEntity
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                costumerAddressId = _address.Id,
                freight = 10,
                orderNumber = "550",
                items = new List<OrderItemEntity>(),
                total = 60,
                subTotal = 50
            });

            var _orderItemEntity = new OrderItemEntity
            {
                Id = Guid.NewGuid(),
                productId = _product.Id,
                orderId = _order.Id,
                quantity = 1,
                freight = 10,
                total = 110,
                unityValue = 100,
            };

            var response = await _orderItemRepository.Create(_orderItemEntity);
            var get = await _orderItemRepository.GetOrderItemByOrderId(response.orderId);

            Assert.IsNotNull(get);

            foreach (var item in get)
            {
                Assert.AreEqual(item.Id, _orderItemEntity.Id);
                Assert.AreEqual(item.productId, _orderItemEntity.productId);
                Assert.AreEqual(item.orderId, _orderItemEntity.orderId);
                Assert.AreEqual(item.quantity, _orderItemEntity.quantity);
                Assert.AreEqual(item.freight, _orderItemEntity.freight);
                Assert.AreEqual(item.total, _orderItemEntity.total);
                Assert.AreEqual(item.unityValue, _orderItemEntity.unityValue);
            }
        }
    }
}
