using Dapper;
using System;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.OrderItems
{
    public class OrderItemRepository
    {
        private readonly MySqlContext _sqlContext;
        public OrderItemRepository(MySqlContext _context)
        {
            this._sqlContext = _context;
        }
        public virtual async Task<OrderItemEntity> Create(OrderItemEntity _orderEntity)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = @"insert into orderitems (id, orderId, productId, quantity, freight, unityValue,total)
                                        values (@Pid, @PorderId, @PproductId, @Pquantity, @Pfreight, @PunityValue,@Ptotal)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _orderEntity.Id,
                        PorderId = _orderEntity.orderId,
                        PproductId = _orderEntity.productId,
                        Pquantity = _orderEntity.quantity,
                        Pfreight = _orderEntity.freight,
                        PunityValue = _orderEntity.unityValue,
                        Ptotal = _orderEntity.total
                    });

                    Console.WriteLine("[OrderItemsRepository][Create] Order was successfully created!");
                    return _orderEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[OrderItemsRepository][Create] Error while trying to create order. " + ex);
                    return new OrderItemEntity();
                }
            }
        }
        public virtual async Task<OrderItemEntity> Update(OrderItemEntity _itemEntity)
        {
            try
            {
                using (var cnx = _sqlContext.Connect())
                {
                    string sqlQuery = @"update orderitems set orderId = @PorderId,
                                                         productId = @PproductId,
                                                         quantity = @Pquantity,
                                                         freight = @Pfreight, 
                                                         unityValue = @PunityValue, 
                                                         total = @Ptotal 
                                                         where id = @Pid";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _itemEntity.Id,
                        PorderId = _itemEntity.orderId,
                        PproductId = _itemEntity.productId,
                        Pquantity = _itemEntity.quantity,
                        Pfreight = _itemEntity.freight,
                        PunityValue = _itemEntity.unityValue,
                        Ptotal = _itemEntity.total,
                    });

                    Console.WriteLine("[OrderItemsRepository][Update] Order item was successfully updated!");
                    return _itemEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OrderItemsRepository][Update] Error while trying to update order item. " + ex);
                return new OrderItemEntity();
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $"delete from orderitems where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    Console.WriteLine("[OrderItemsRepository][Delete] Order item was successfully deleted!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[OrderItemsRepository][Delete] Error while trying to delete order item. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<OrderItemEntity> Get(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $@"select id, orderId, productId, quantity, freight, unityValue,total, CreationDate, updatedDate
                                                from orderitems
                                                where id = '{id}'";
                    var query = await cnx.QueryFirstOrDefaultAsync<OrderItemEntity>(sqlQuery);
                    Console.WriteLine("[OrderItemsRepository][Get] Order items was successfully consulted!");
                    return query;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[OrderItemsRepository][Get] Error while trying to consult order item. " + ex);
                    return new OrderItemEntity();
                }
            }
        }
        public virtual async Task<OrderItemEntity> GetOrderItemByProductId(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id, orderId, productId, quantity, freight, unityValue,total, CreationDate, updatedDate
                                                from orderitems
                                                where productId = '{id}'";
                var query = await cnx.QueryFirstOrDefaultAsync<OrderItemEntity>(sqlQuery);

                return query;
            }
        }
        public virtual async Task<List<OrderItemEntity>> GetOrderItemByOrderId(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id, orderId, productId, quantity, freight, unityValue,total, CreationDate, updatedDate
                                                from orderitems
                                                where orderId = '{id}'";
                var query = await cnx.QueryAsync<OrderItemEntity>(sqlQuery);

                return query.AsList();
            }
        }
    }
}
