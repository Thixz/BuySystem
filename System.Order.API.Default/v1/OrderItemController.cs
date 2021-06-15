using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Contracts.Request.OrderItems;
using System.Application.Data.Entities.OrderItems;
using System.Application.Helpers;
using System.Application.Services.Adresses;
using System.Application.Services.OrderItems;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Order.API.Default.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;
        public OrderItemController(OrderItemService orderService)
        {
            this._orderItemService = orderService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] OrderItemPostRequest _postRequest)
        {
            var result = await _orderItemService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrderItemPutRequest _putRequest)
        {
            var result = await _orderItemService.Update(_putRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _orderItemService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _orderItemService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
