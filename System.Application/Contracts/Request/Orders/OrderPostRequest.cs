using System;
using System.Application.Data.Entities.OrderItems;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Request.Orders
{
    public class OrderPostRequest
    {
        public string orderNumber { get; set; }
        public Guid costumerId { get; set; }
        public Guid costumerAddressId { get; set; }
        public double subTotal { get; set; }
        public double freight { get; set; }
        public double total { get; set; }
        public List<OrderItemEntity> items { get; set; }
    }
}
