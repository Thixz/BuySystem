using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Helpers;
using System.Application.Services.Costumers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Costumer.API.Default.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CostumerController : ControllerBase
    {
        private readonly CostumerService _costumerService;
        public CostumerController(CostumerService costumerService)
        {
            this._costumerService = costumerService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] CostumerPostRequest _postRequest)
        {
            var result = await _costumerService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CostumerPutRequest _putRequest)
        {
            var result = await _costumerService.Update(_putRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _costumerService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _costumerService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
