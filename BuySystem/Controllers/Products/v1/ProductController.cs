using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Products;
using System.Application.Helpers;
using System.Application.Services.Products;
using System.Collections.Generic;
using System.Text;

namespace System.API.Default.Controllers.Products.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            this._productService = productService;
        }

        [HttpPost("Create")]
        public IActionResult Post([FromBody] ProductPostRequest _postRequest)
        {
            return _productService.Create(_postRequest).Convert();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] ProductPutRequest _putRequest)
        {
            return _productService.Update(_putRequest).Convert();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            return _productService.Delete(id).Convert();
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] Guid id)
        {
            return _productService.Get(id).Convert();
        }
    }
}
