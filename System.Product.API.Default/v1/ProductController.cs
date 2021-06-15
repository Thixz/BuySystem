using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Request.Products;
using System.Application.Helpers;
using System.Application.Services.Products;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Product.API.Default.v1
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
        public virtual async Task<IActionResult> Post([FromBody] ProductPostRequest _postRequest)
        {
            var result = await _productService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProductPutRequest _putRequest)
        {
            var result = await _productService.Update(_putRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _productService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _productService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
