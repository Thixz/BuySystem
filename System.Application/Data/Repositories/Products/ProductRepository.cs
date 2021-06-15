using Dapper;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.Entities.Products;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.Products
{
    public class ProductRepository
    {
        private readonly MySqlContext _sqlContext;
        public ProductRepository(MySqlContext _context)
        {
            this._sqlContext = _context;
        }

        public virtual async Task<ProductEntity> Create(ProductEntity _productEntity)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = @"insert into products (id, productName, productCode, quantity, productDescription, model, productValue)
                                        values (@Pid, @PproductName, @PproductCode, @Pquantity, @PproductDescription, @Pmodel, @PproductValue)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _productEntity.Id,
                        PproductName = _productEntity.productName,
                        PproductCode = _productEntity.productCode,
                        Pquantity = _productEntity.quantity,
                        PproductDescription = _productEntity.productDescription,
                        Pmodel = _productEntity.model,
                        PproductValue = _productEntity.productValue,
                    });

                    Console.WriteLine("[ProductRepository][Create] Product was successfully created!");
                    return _productEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ProductRepository][Create] Error while trying to create product. " + ex);
                    return new ProductEntity();
                }
            }
        }

        public virtual async Task<ProductEntity> Update(ProductEntity _productEntity)
        {
            try
            {
                using (var cnx = _sqlContext.Connect())
                {
                    string sqlQuery = @"update products  set productName = @PproductName,
                                                         productCode = @PproductCode,
                                                         quantity = @Pquantity,
                                                         productDescription = @PproductDescription, 
                                                         model = @Pmodel, 
                                                         productValue = @PproductValue 
                                                         where id = @Pid";

                   await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _productEntity.Id,
                        Pproductname = _productEntity.productName,
                        PproductCode = _productEntity.productCode,
                        Pquantity = _productEntity.quantity,
                        PproductDescription = _productEntity.productDescription,
                        Pmodel = _productEntity.model,
                        PproductValue = _productEntity.productValue,
                    });

                    Console.WriteLine("[ProductRepository][Update] Product was successfully updated!");
                    return _productEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ProductRepository][Update] Error while trying to update product. " + ex);
                return new ProductEntity();
            }
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $"delete from products where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    Console.WriteLine("[ProductRepository][Delete] Product was successfully deleted!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ProductRepository][Delete] Error while trying to delete product. " + ex);
                    return false;
                }
            }
        }

        public virtual async Task<ProductEntity> Get(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $@"select id,productName, productCode, quantity, productDescription,model,productValue,
                                                creationDate,updatedDate from products 
                                                where id = '{id}'";
                    var query = await cnx.QueryFirstOrDefaultAsync<ProductEntity>(sqlQuery);
                    Console.WriteLine("[ProductRepository][Get] Product was successfully consulted!");
                    return query;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ProductRepository][Get] Error while trying to consult product. " + ex);
                    return new ProductEntity();
                }
            }
        }

        public virtual async Task<ProductEntity> GetByCode(string code)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id,productName, productCode, quantity, productDescription,model,productValue,
                                                creationDate,updatedDate from products 
                                                where productCode = '{code}'";
                var query = await cnx.QueryFirstOrDefaultAsync<ProductEntity>(sqlQuery);
                return query;
            }
        }
    }
}
