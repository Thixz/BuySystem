using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Orders;
using System.Application.Helpers;
using System.Application.Services.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Order.API.Default.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] OrderPostRequest _postRequest)
        {
            var result = await _orderService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _orderService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _orderService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
