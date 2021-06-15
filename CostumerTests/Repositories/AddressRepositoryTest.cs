using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Data.Entities.Adresses;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.MySql;
using System.Application.Data.Repositories;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Repositories
{
    [TestClass]
    public class AddressRepositoryTest : BuySystemTests
    {
        [TestInitialize]
        public void Initiliaze()
        {
            var connection = _context.Connect();
            connection.ExecuteAsync("delete from buysystem.addresses");
            connection.ExecuteAsync("delete from buysystem.costumers");
        }

        [TestMethod]
        public async Task Address_Repository_Post_Success()
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
            var _costumer = await _costumerRepository.Create(_costumerEntity);

            var _addressEntity = new AdressEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil"
            };
            var response = await _adressRepository.Create(_addressEntity);

            Assert.IsNotNull(response);

            Assert.AreEqual(_addressEntity.country, response.country);
            Assert.AreEqual(_addressEntity.postalCode, response.postalCode);
            Assert.AreEqual(_addressEntity.neighborhood, response.neighborhood);
            Assert.AreEqual(_addressEntity.addressNumber, response.addressNumber);
            Assert.AreEqual(_addressEntity.costumerId, response.costumerId);
        }
        [TestMethod]
        public async Task Address_Repository_Post_Error()
        {
            var _addressEntity = new AdressEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = Guid.NewGuid(),
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil",
            };
            var response = await _adressRepository.Create(_addressEntity);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Address_Repository_Put_Success()
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
            var _costumer = await _costumerRepository.Create(_costumerEntity);

            var _addressEntity = new AdressEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil",
            };
            var response = await _adressRepository.Create(_addressEntity);

            var _updated = await _adressRepository.Update(response);

            Assert.IsNotNull(_updated);
        }
        [TestMethod]
        public async Task Address_Repository_Put_Error()
        {
            var response = await _adressRepository.Update(null as AdressEntity);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Address_Repository_Delete_Success()
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
            var _costumer = await _costumerRepository.Create(_costumerEntity);

            var _addressEntity = new AdressEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil",
            };
            var _response = await _adressRepository.Create(_addressEntity);

            var _deleted = await _adressRepository.Delete(_response.Id);

            Assert.IsTrue(_deleted);
        }
        [TestMethod]
        public async Task Address_Repository_Get_Success()
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
            var _costumer = await _costumerRepository.Create(_costumerEntity);

            var _addressEntity = new AdressEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil",
            };
            var _response = await _adressRepository.Create(_addressEntity);

            var _get = await _adressRepository.Get(_response.Id);

            Assert.IsNotNull(_get);

            Assert.AreEqual(_addressEntity.country, _get.country);
            Assert.AreEqual(_addressEntity.postalCode, _get.postalCode);
            Assert.AreEqual(_addressEntity.neighborhood, _get.neighborhood);
            Assert.AreEqual(_addressEntity.addressNumber, _get.addressNumber);
            Assert.AreEqual(_addressEntity.costumerId, _get.costumerId);
        }
        [TestMethod]
        public async Task Address_Repository_GetAddressByCostumerId_Success()
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
            var _costumer = await _costumerRepository.Create(_costumerEntity);

            var _addressEntity = new AdressEntity()
            {
                Id = Guid.NewGuid(),
                costumerId = _costumer.Id,
                address = "Rua das ruas",
                addressNumber = "1001",
                neighborhood = "Bairro",
                postalCode = "03410101",
                country = "Brasil",
            };
            var _response = await _adressRepository.Create(_addressEntity);

            var _get = await _adressRepository.GetAddressByCostumerId(_costumer.Id);

            Assert.IsNotNull(_get);

            Assert.AreEqual(_addressEntity.country, _get.country);
            Assert.AreEqual(_addressEntity.postalCode, _get.postalCode);
            Assert.AreEqual(_addressEntity.neighborhood, _get.neighborhood);
            Assert.AreEqual(_addressEntity.addressNumber, _get.addressNumber);
            Assert.AreEqual(_addressEntity.costumerId, _get.costumerId);
        }
    }
}
