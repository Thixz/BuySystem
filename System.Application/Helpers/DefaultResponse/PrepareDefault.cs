using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Helpers.Costumers
{
    public class PrepareDefault
    {
        protected DefaultResponse<T> SuccessResponse<T>(T data)
        {
            return new DefaultResponse<T>(data)
            {
                success = true
            };
        }

        protected DefaultResponse<T> ErrorResponse<T>(T data)
        {
            return new DefaultResponse<T>(data)
            {
                success = false
            };
        }
    }
}
