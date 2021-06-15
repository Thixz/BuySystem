using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Helpers;
using System.Application.Services.Adresses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Costumer.API.Default.v1
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
        public async Task<IActionResult> Post([FromBody] AdressPostRequest _postRequest)
        {
            var result = await _adressService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AdressPutRequest _putRequest)
        {
            var result = await _adressService.Update(_putRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _adressService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _adressService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
