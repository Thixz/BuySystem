using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Helpers;
using System.Application.Services.Adresses;
using System.Collections.Generic;
using System.Text;

namespace System.API.Default.Controllers.Adresses.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AdressController : ControllerBase
    {
        private readonly AdressService _adressService;
        public AdressController(AdressService productService)
        {
            this._adressService = productService;
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] AdressPostRequest _postRequest)
        {
            return _adressService.Create(_postRequest).Convert();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] AdressPutRequest _putRequest)
        {
            return _adressService.Update(_putRequest).Convert();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            return _adressService.Delete(id).Convert();
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid id)
        {
            return _adressService.Get(id).Convert();
        }
    }
}

