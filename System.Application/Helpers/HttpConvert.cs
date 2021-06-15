using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Helpers.Costumers;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Helpers
{
    public static class HttpConvert
    {
        public static IActionResult Convert(this DefaultResponse _defaultResponse)
        {
            if (_defaultResponse.success)
                return new ObjectResult(_defaultResponse) { StatusCode = (int)HttpStatusCode.OK };

            return new ObjectResult(_defaultResponse) { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
