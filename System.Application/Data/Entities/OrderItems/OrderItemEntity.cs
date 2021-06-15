using System;
using System.Application.Contracts.Request.OrderItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.OrderItems
{
    [Table("orderitems")]
    public class OrderItemEntity
    {
        public OrderItemEntity(OrderItemPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.orderId = _postRequest.orderId;
            this.productId = _postRequest.productId;
            this.quantity = _postRequest.quantity;
            this.freight = _postRequest.freight;
            this.unityValue = _postRequest.unityValue;
            this.total = _postRequest.total;
        }
        public OrderItemEntity(OrderItemPutRequest _putRequest)
        {
            this.Id = _putRequest.id;
            this.orderId = _putRequest.orderId;
            this.productId = _putRequest.productId;
            this.quantity = _putRequest.quantity;
            this.freight = _putRequest.freight;
            this.unityValue = _putRequest.unityValue;
            this.total = _putRequest.total;
        }
        public OrderItemEntity()
        {

        }
        ///<summary>
        ///Order Item Id
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Order Id
        ///</summary>
        [Column("orderId")]
        public Guid orderId { get; set; }
        ///<summary>
        ///Product Id
        ///</summary>
        [Column("productId")]
        public Guid productId { get; set; }
        ///<summary>
        ///Item Quantity
        ///</summary>
        [Column("quantity")]
        public double quantity { get; set; }
        ///<summary>
        ///Item Freight
        ///</summary>
        [Column("freight")]
        public double freight { get; set; }
        ///<summary>
        ///Item Unity Value
        ///</summary>
        [Column("unityValue")]
        public double unityValue { get; set; }
        ///<summary>
        ///Item Total
        ///</summary>
        [Column("total")]
        public double total { get; set; }
        ///<summary>
        ///Created
        ///</summary>
        [Column("creationDate")]
        public DateTime creationDate { get; set; }
        ///<summary>
        ///Updated
        ///</summary>
        [Column("updatedDate")]
        public DateTime updatedDate { get; set; }
    }
}
