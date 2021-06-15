using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Orders;
using System.Application.Helpers;
using System.Application.Services.Orders;
using System.Collections.Generic;
using System.Text;

namespace System.API.Default.Controllers.Orders.v1
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
        public IActionResult Post([FromBody] OrderPostRequest _postRequest)
        {
            return _orderService.Create(_postRequest).Convert();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            return _orderService.Delete(id).Convert();
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid id)
        {
            return _orderService.Get(id).Convert();
        }

    }
}
