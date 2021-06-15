using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Request.Costumers
{
    public class CostumerPostRequest
    {
        public string costumerName { get; set; }
        public string document { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public DateTime birthdate { get; set; }
        public int gender { get; set; }
    }
}
