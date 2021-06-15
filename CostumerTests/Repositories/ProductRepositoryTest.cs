using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Data.Entities.Products;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuySytemTests.Repositories
{
    [TestClass]
    public class ProductRepositoryTest : BuySystemTests
    {
        [TestInitialize]
        public void Initiliaze()
        {
            var connection = _context.Connect();
            connection.ExecuteAsync("delete from buysystem.products");
        }

        [TestMethod]
        public async Task Product_Repository_Post_Success()
        {
            var _productEntity = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
            };
            var response = await _productRepository.Create(_productEntity);

            Assert.IsNotNull(response);

            Assert.AreEqual(_productEntity.Id, response.Id);
            Assert.AreEqual(_productEntity.productCode, response.productCode);
            Assert.AreEqual(_productEntity.model, response.model);
            Assert.AreEqual(_productEntity.productDescription, response.productDescription);
            Assert.AreEqual(_productEntity.productName, response.productName);
            Assert.AreEqual(_productEntity.productValue, response.productValue);
            Assert.AreEqual(_productEntity.quantity, _productEntity.quantity);
        }
        [TestMethod]
        public async Task Product_Repository_Post_Error()
        {
            var response = await _productRepository.Create(null as ProductEntity);

            Assert.AreEqual(response.Id, Guid.Empty);
        }
        [TestMethod]
        public async Task Product_Repository_Put_Success()
        {
            var _productEntity = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
            };
            var response = await _productRepository.Create(_productEntity);

            var _updated = await _productRepository.Update(response);

            Assert.IsNotNull(_updated);

            Assert.AreEqual(_updated.Id, response.Id);
            Assert.AreEqual(_updated.productCode, response.productCode);
            Assert.AreEqual(_updated.model, response.model);
            Assert.AreEqual(_updated.productDescription, response.productDescription);
            Assert.AreEqual(_updated.productName, response.productName);
            Assert.AreEqual(_updated.productValue, response.productValue);
            Assert.AreEqual(_updated.quantity, _productEntity.quantity);
        }
        [TestMethod]
        public async Task Product_Repository_Put_Error()
        {
            var _updated = await _productRepository.Update(null as ProductEntity);

            Assert.AreEqual(_updated.Id,Guid.Empty);
        }
        [TestMethod]
        public async Task Product_Repository_Delete_Success()
        {
            var _productEntity = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
            };
            var response = await _productRepository.Create(_productEntity);

            var deleted = await _productRepository.Delete(response.Id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public async Task Product_Repository_Get_Success()
        {
            var _productEntity = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
            };
            var response = await _productRepository.Create(_productEntity);

            var get = await _productRepository.Get(response.Id);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.productCode, response.productCode);
            Assert.AreEqual(get.model, response.model);
            Assert.AreEqual(get.productDescription, response.productDescription);
            Assert.AreEqual(get.productName, response.productName);
            Assert.AreEqual(get.productValue, response.productValue);
            Assert.AreEqual(get.quantity, _productEntity.quantity);
        }
        [TestMethod]
        public async Task Product_Repository_GetByProductCode_Success()
        {
            var _productEntity = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                productCode = "15154",
                model = "teste",
                productDescription = "testetes",
                productName = "teste",
                productValue = 100,
                quantity = 10,
            };
            var response = await _productRepository.Create(_productEntity);

            var get = await _productRepository.GetByCode(response.productCode);

            Assert.IsNotNull(get);

            Assert.AreEqual(get.Id, response.Id);
            Assert.AreEqual(get.productCode, response.productCode);
            Assert.AreEqual(get.model, response.model);
            Assert.AreEqual(get.productDescription, response.productDescription);
            Assert.AreEqual(get.productName, response.productName);
            Assert.AreEqual(get.productValue, response.productValue);
            Assert.AreEqual(get.quantity, _productEntity.quantity);
        }
    }
}
