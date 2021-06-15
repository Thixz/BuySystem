using System;
using System.Application.Contracts.Request.Adress;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Adresses
{
    [Table("adresses")]
    public class AdressEntity
    {
        public AdressEntity(AdressPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.costumerId = _postRequest.costumerId;
            this.address = _postRequest.address;
            this.addressNumber = _postRequest.addressNumber;
            this.neighborhood = _postRequest.neighborhood;
            this.postalCode = _postRequest.postalCode;
            this.country = _postRequest.country;
        }
        public AdressEntity(AdressPutRequest _putRequest)
        {
            this.Id = _putRequest.Id;
            this.costumerId = _putRequest.costumerId;
            this.address = _putRequest.address;
            this.addressNumber = _putRequest.addressNumber;
            this.neighborhood = _putRequest.neighborhood;
            this.postalCode = _putRequest.postalCode;
            this.country = _putRequest.country;
        }
        public AdressEntity()
        {

        }
        ///<summary>
        ///Adress Id
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Costumer ID
        ///</summary>
        [Column("costumerId")]
        public Guid costumerId { get; set; }
        ///<summary>
        ///Adress
        ///</summary>
        [Column("address")]
        public string address { get; set; }
        ///<summary>
        ///Adress number
        ///</summary>
        [Column("addressNumber")]
        public string addressNumber { get; set; }
        ///<summary>
        ///Adress Neighborhood
        ///</summary>
        [Column("neighborhood")]
        public string neighborhood { get; set; }
        ///<summary>
        ///Adress Postal Code
        ///</summary>
        [Column("postalCode")]
        public string postalCode { get; set; }
        ///<summary>
        ///Adress Country
        ///</summary>
        [Column("country")]
        public string country { get; set; }
        ///<summary>
        ///Created
        ///</summary>
        [Column("creationDate")]
        public DateTime creationDate { get; set; }
        ///<Updated
        ///</summary>
        [Column("updatedDate")]
        public DateTime updatedDate { get; set; }
    }
}
