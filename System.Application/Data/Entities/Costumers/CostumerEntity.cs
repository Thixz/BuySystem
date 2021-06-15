using System;
using System.Application.Contracts.Request.Costumers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Costumers
{
    [Table("costumers")]
    public class CostumerEntity
    {
        public CostumerEntity(CostumerPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.costumerName = _postRequest.costumerName;
            this.document = _postRequest.document;
            this.phoneNumber = _postRequest.phoneNumber;
            this.email = _postRequest.email;
            this.birthdate = _postRequest.birthdate;
            this.gender = _postRequest.gender;
        }
        public CostumerEntity(CostumerPutRequest _putRequest)
        {
            this.Id = _putRequest.id;
            this.costumerName = _putRequest.costumerName;
            this.document = _putRequest.document;
            this.phoneNumber = _putRequest.phoneNumber;
            this.email = _putRequest.email;
            this.birthdate = _putRequest.birthdate;
            this.gender = _putRequest.gender;
        }
        public CostumerEntity()
        {

        }
        ///<summary>
        ///Costumer Id
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Costumer Name
        ///</summary>
        [Column("costumerName")]
        public string costumerName { get; set; }
        ///<summary>
        ///Costumer cpf
        ///</summary>
        [Column("document")]
        public string document { get; set; }
        ///<summary>
        ///Phone Number
        ///</summary>
        [Column("phoneNumber")]
        public string phoneNumber { get; set; }
        ///<summary>
        ///Costumer Email
        ///</summary>
        [Column("email")]
        public string email { get; set; }
        ///<summary>
        ///Costumer Birthdate
        ///</summary>
        [Column("birthdate")]
        public DateTime birthdate { get; set; }
        ///<summary>
        ///Costumer Gender
        ///</summary>
        [Column("gender")]
        public int gender { get; set; }
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
