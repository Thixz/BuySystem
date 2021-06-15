using System;
using System.Application.Contracts.Request.Orders;
using System.Application.Data.Entities.OrderItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Orders
{
    [Table ("orders")]
    public class OrderEntity
    {
        public OrderEntity(OrderPostRequest _orderPost)
        {
            this.Id = Guid.NewGuid();
            this.orderNumber = _orderPost.orderNumber;
            this.costumerId = _orderPost.costumerId;
            this.costumerAddressId = _orderPost.costumerAddressId;
            this.subTotal = _orderPost.subTotal;
            this.freight = _orderPost.freight;
            this.total = _orderPost.total;
            this.items = _orderPost.items;
        }
        public OrderEntity()
        {

        }
        ///<summary>
        ///Order Id
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Order Number
        ///</summary>
        [Column("orderNumber")]
        public string orderNumber { get; set; }
        ///<summary>
        ///Costumer Id
        ///</summary>
        [Column("costumerId")]
        public Guid costumerId { get; set; }
        ///<summary>
        ///Costumer Adress Id
        ///</summary>
        [Column("costumerAddressId")]
        public Guid costumerAddressId { get; set; }
        ///<summary>
        ///Order Subtotal
        ///</summary>
        [Column("subTotal")]
        public double subTotal { get; set; }
        ///<summary>
        ///Order Freight
        ///</summary>
        [Column("freight")]
        public double freight { get; set; }
        ///<summary>
        ///Order Total
        ///</summary>
        [Column("total")]
        public double total { get; set; }
        ///<summary>
        ///Order Total
        ///</summary>
        [Column("items")]
        public List<OrderItemEntity> items { get; set; }
        ///<summary>
        ///Created
        ///</summary>
        [Column("creationDate")]
        public DateTime creationDate { get; set; }
    }
}
