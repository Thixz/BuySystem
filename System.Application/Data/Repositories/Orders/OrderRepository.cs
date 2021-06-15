using Dapper;
using System;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Orders;
using System.Application.Data.MySql;
using System.Application.Data.Repositories.OrderItems;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.Orders
{
    public class OrderRepository
    {
        private readonly MySqlContext _sqlContext;
        public OrderRepository(MySqlContext _context)
        {
            this._sqlContext = _context;
        }
        public virtual async Task<OrderEntity> Create(OrderEntity _orderEntity)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = @"insert into orders (id, orderNumber, costumerId, costumerAddressId, subTotal, freight, total)
                                        values (@Pid, @PorderNumber, @PcostumerId, @PcostumerAddressId, @PsubTotal, @Pfreight,@Ptotal)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _orderEntity.Id,
                        PorderNumber = _orderEntity.orderNumber,
                        PcostumerId = _orderEntity.costumerId,
                        PcostumerAddressId = _orderEntity.costumerAddressId,
                        PsubTotal = _orderEntity.subTotal,
                        Pfreight = _orderEntity.freight,
                        Ptotal = _orderEntity.total,
                        Pitems = _orderEntity.items
                    });

                    Console.WriteLine("[OrderRepository][Create] Order was successfully created!");
                    return _orderEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[OrderRepository][Create] Error while trying to create order. " + ex);
                    return new OrderEntity();
                }
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $"delete from orders where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    Console.WriteLine("[OrderRepository][Delete] Order was successfully deleted!");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[OrderRepository][Delete] Error while trying to delete order. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<OrderEntity> Get(Guid id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                try
                {
                    string sqlQuery = $@"select id,orderNumber,costumerId,costumerAddressId,subTotal,freight,
                                                total,creationDate from orders 
                                                where id = '{id}'";
                    var query = await cnx.QueryFirstOrDefaultAsync<OrderEntity>(sqlQuery);
                    Console.WriteLine("[OrderRepository][Get] Order was successfully consulted!");
                    return query;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[OrderRepository][Get] Error while trying to consult order. " + ex);
                    return new OrderEntity();
                }
            }
        }
        public virtual async Task<OrderEntity> GetOrderByNumber(string orderNumber)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id,orderNumber,costumerId,costumerAddressId,subTotal,freight,
                                                total,creationDate from orders 
                                                where orderNumber = '{orderNumber}'";

                return await cnx.QueryFirstOrDefaultAsync<OrderEntity>(sqlQuery);
            }
        }
        public virtual async Task<OrderEntity> GetOrderByCostumerId(Guid Id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id,orderNumber,costumerId,costumerAddressId,subTotal,freight,
                                                total,creationDate from orders 
                                                where costumerId = '{Id}'";

               return await cnx.QueryFirstOrDefaultAsync<OrderEntity>(sqlQuery);
            }
        }
        public virtual async Task<OrderEntity> GetOrderByAddressId(Guid Id)
        {
            using (var cnx = _sqlContext.Connect())
            {
                string sqlQuery = $@"select id,orderNumber,costumerId,costumerAddressId,subTotal,freight,
                                                total,creationDate from orders 
                                                where costumerAddressId = '{Id}'";

                return await cnx.QueryFirstOrDefaultAsync<OrderEntity>(sqlQuery);
            }
        }
    }
}
