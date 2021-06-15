using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Request.Adress
{
    public class AdressPutRequest
    {
        public Guid Id { get; set; }
        public Guid costumerId { get; set; }
        public string address { get; set; }
        public string addressNumber { get; set; }
        public string neighborhood { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
    }
}
