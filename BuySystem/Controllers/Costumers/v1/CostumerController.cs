using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Helpers;
using System.Application.Services.Costumers;
using System.Collections.Generic;
using System.Text;

namespace System.API.Default.Controllers.Costumers.v1
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
        public IActionResult Post([FromBody] CostumerPostRequest _postRequest)
        {
            return _costumerService.Create(_postRequest).Convert();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] CostumerPutRequest _putRequest)
        {
            return _costumerService.Update(_putRequest).Convert();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            return _costumerService.Delete(id).Convert();
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid id)
        {
            return _costumerService.Get(id).Convert();
        }
    }
}
