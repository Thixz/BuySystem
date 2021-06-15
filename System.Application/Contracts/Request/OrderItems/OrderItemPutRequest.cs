using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Request.OrderItems
{
    public class OrderItemPutRequest
    {
        public Guid id { get; set; }
        public Guid orderId { get; set; }
        public Guid productId { get; set; }
        public double quantity { get; set; }
        public double freight { get; set; }
        public double unityValue { get; set; }
        public double total { get; set; }
    }
}
