using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Request.Products
{
    public class ProductPutRequest
    {
        public Guid id { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public int quantity { get; set; }
        public string productDescription { get; set; }
        public string model { get; set; }
        public double productValue { get; set; }
    }
}
