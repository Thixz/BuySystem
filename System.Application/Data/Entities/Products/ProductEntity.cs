using System;
using System.Application.Contracts.Request.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Products
{
    [Table("products")]
    public class ProductEntity
    {
        public ProductEntity(ProductPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.productName = _postRequest.productName;
            this.productCode = _postRequest.productCode;
            this.quantity = _postRequest.quantity;
            this.productDescription = _postRequest.productDescription;
            this.model = _postRequest.model;
            this.productValue = _postRequest.productValue;
        }
        public ProductEntity(ProductPutRequest _putRequest)
        {
            this.Id = _putRequest.id;
            this.productName = _putRequest.productName;
            this.productCode = _putRequest.productCode;
            this.quantity = _putRequest.quantity;
            this.productDescription = _putRequest.productDescription;
            this.model = _putRequest.model;
            this.productValue = _putRequest.productValue;
        }
        public ProductEntity()
        {

        }
        ///<summary>
        ///Costumer Id
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Product Name
        ///</summary>
        [Column("productName")]
        public string productName { get; set; }
        ///<summary>
        ///Product Quantity
        ///</summary>
        [Column("productCode")]
        public string productCode { get; set; }
        ///<summary>
        ///Product Quantity
        ///</summary>
        [Column("quantity")]
        public int quantity { get; set; }
        ///<summary>
        ///Product Description
        ///</summary>
        [Column("productDescription")]
        public string productDescription { get; set; }
        ///<summary>
        ///Product model
        ///</summary>
        [Column("model")]
        public string model { get; set; }
        ///<summary>
        ///Product Value
        ///</summary>
        [Column("productValue")]
        public double productValue { get; set; }
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
