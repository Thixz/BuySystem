using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Helpers.Costumers
{
    public class DefaultResponse
    {
        public bool success { get; set; }
    }

    public class DefaultResponse<T> : DefaultResponse
    {
        public DefaultResponse(T data)
        {
            this.data = data;
        }
        public T data { get;private set; }
        
    }
}
