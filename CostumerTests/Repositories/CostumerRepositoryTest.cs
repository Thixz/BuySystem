using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Repositories
{
    [TestClass]
    public class CostumerRepositoryTest : BuySystemTests
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
        public async Task Costumer_Repository_Post_Success()
        {
            var _costumerEntity = new CostumerEntity()
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09")
            };
            var response = await _costumerRepository.Create(_costumerEntity);

            Assert.IsNotNull(response);

            Assert.AreEqual(_costumerEntity.Id, response.Id);
            Assert.AreEqual(_costumerEntity.costumerName, response.costumerName);
            Assert.AreEqual(_costumerEntity.email, response.email);
            Assert.AreEqual(_costumerEntity.document, response.document);
            Assert.AreEqual(_costumerEntity.phoneNumber, response.phoneNumber);
            Assert.AreEqual(_costumerEntity.gender, response.gender);
            Assert.AreEqual(_costumerEntity.birthdate, response.birthdate);
        }
        [TestMethod]
        public async Task Costumer_Repository_Post_Error()
        {
            var response = await _costumerRepository.Create(null as CostumerEntity);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Costumer_Repository_Put_Success()
        {
            var _costumerEntity = new CostumerEntity()
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09"),
            };
            var response = await _costumerRepository.Create(_costumerEntity);

            var _updated = await _costumerRepository.Update(response);

            Assert.IsNotNull(_updated);

            Assert.AreEqual(_updated.Id, response.Id);
            Assert.AreEqual(_updated.costumerName, response.costumerName);
            Assert.AreEqual(_updated.email, response.email);
            Assert.AreEqual(_updated.document, response.document);
            Assert.AreEqual(_updated.phoneNumber, response.phoneNumber);
            Assert.AreEqual(_updated.gender, response.gender);
            Assert.AreEqual(_updated.birthdate, response.birthdate);
        }
        [TestMethod]
        public async Task Costumer_Repository_Put_Error()
        {
            var _updated = await _costumerRepository.Update(null as CostumerEntity);
            Assert.AreEqual(_updated.Id , Guid.Empty);
        }
        [TestMethod]
        public async Task Costumer_Repository_Delete_Success()
        {
            var _costumerEntity = new CostumerEntity()
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09"),
            };
            var response = await _costumerRepository.Create(_costumerEntity);

            var _deleted = await _costumerRepository.Delete(response.Id);

            Assert.IsTrue(_deleted);
        }
        [TestMethod]
        public async Task Costumer_Repository_Get_Success()
        {
            var _costumerEntity = new CostumerEntity()
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09"),
            };
            var response = await _costumerRepository.Create(_costumerEntity);

            var _get = await _costumerRepository.Get(response.Id);

            Assert.IsNotNull(_get);

            Assert.AreEqual(_costumerEntity.Id, _get.Id);
            Assert.AreEqual(_costumerEntity.costumerName, _get.costumerName);
            Assert.AreEqual(_costumerEntity.email, _get.email);
            Assert.AreEqual(_costumerEntity.document, _get.document);
            Assert.AreEqual(_costumerEntity.phoneNumber, _get.phoneNumber);
            Assert.AreEqual(_costumerEntity.gender, _get.gender);
            Assert.AreEqual(_costumerEntity.birthdate, _get.birthdate);
        }
        [TestMethod]
        public async Task Costumer_Repository_GetByDocument_Success()
        {
            var _costumerEntity = new CostumerEntity()
            {
                Id = Guid.NewGuid(),
                costumerName = "teste",
                email = "teste@teste",
                document = "5165165",
                phoneNumber = "51545454",
                gender = 2,
                birthdate = DateTime.Parse("1994-12-09"),
            };
            var response = await _costumerRepository.Create(_costumerEntity);

            var _get = await _costumerRepository.GetByDocument(response.document);

            Assert.IsNotNull(_get);

            Assert.AreEqual(response.Id, _get.Id);
            Assert.AreEqual(response.costumerName, _get.costumerName);
            Assert.AreEqual(response.email, _get.email);
            Assert.AreEqual(response.document, _get.document);
            Assert.AreEqual(response.phoneNumber, _get.phoneNumber);
            Assert.AreEqual(response.gender, _get.gender);
            Assert.AreEqual(response.birthdate, _get.birthdate);
        }
    }
}
