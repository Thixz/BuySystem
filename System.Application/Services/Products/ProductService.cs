using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Contracts.Request.Products;
using System.Application.Data.Entities.Costumers;
using System.Application.Data.Entities.Products;
using System.Application.Data.Repositories.Costumers;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Products;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Application.Services.Adresses;
using System.Application.Validators.Products;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Products
{

    public class ProductService : PrepareDefault
    {
        private readonly ProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly OrderItemRepository _itemRepository;

        public ProductService(ProductRepository repository, ILogger<ProductService> logger, OrderItemRepository itemRepository)
        {
            this._productRepository = repository;
            this._logger = logger;
            this._itemRepository = itemRepository;
        }

        public async Task<DefaultResponse> Create(ProductPostRequest _postRequest)
        {
            var validator = new ProductPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[ProductService][Create] Error while trying to create Product: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await _productRepository.GetByCode(_postRequest.productCode) != null)
            {
                _logger.LogError($"[ProductService][Create] A product with this code already exists.");
                return ErrorResponse(ProductErrors.Product_Post_400_ProductCode_Cannot_Be_Duplicated.GetDescription());
            }

            var result = await _productRepository.Create(new ProductEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                _logger.LogError($"[ProductService][Create] It was impossible to rech database.");
                return ErrorResponse(ProductErrors.Product_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }

        public async Task<DefaultResponse> Update(ProductPutRequest _putRequest)
        {
            if (_putRequest.id == Guid.Empty)
            {
                _logger.LogError($"[ProductService][Updated] ID was null or empty.");
                return ErrorResponse(ProductErrors.Product_Put_400_ProductCode_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new ProductPutRequestValidator().Validate(_putRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[ProductService][Update] Error while trying to create Product: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            var code = await _productRepository.GetByCode(_putRequest.productCode);
            if (code != null && _putRequest.id != code.Id)
            {
                _logger.LogError($"[ProductService][Update] A product with this code already exists.");
                return ErrorResponse(ProductErrors.Product_Put_400_ProductCode_Cannot_Be_Duplicated.GetDescription());
            }

            if (await _productRepository.Get(_putRequest.id) == null)
            {
                _logger.LogError($"[ProductService][Update] It was impossible to find a product with this ID.");
                return ErrorResponse(ProductErrors.Product_Put_400_ProductID_DoesnotExists.GetDescription());
            }

            var result = await _productRepository.Update(new ProductEntity(_putRequest));
            if (result.Id == Guid.Empty)
            {
                _logger.LogError($"[ProductService][Update] Database Error.");
                return ErrorResponse(ProductErrors.Product_Put_400_Update_Error.GetDescription());
            }

            return SuccessResponse(result);
        }

        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"[ProductService][Delete] ID was null or empty.");
                return ErrorResponse(ProductErrors.Product_Delete_400_ProductID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await _itemRepository.GetOrderItemByProductId(id) != null)
            {
                _logger.LogError($"[ProductService][Delete] An order item related to this product exists.");
                return ErrorResponse(ProductErrors.Product_Delete_400_ForeignKey_Error.GetDescription());
            }

            if (await _productRepository.Get(id) == null)
            {
                _logger.LogError($"[ProductService][Delete] It was impossible to find a costumer with this ID.");
                return ErrorResponse(ProductErrors.Product_Delete_400_ProductID_DoesnotExists.GetDescription());
            }

            if (!await _productRepository.Delete(id))
            {
                _logger.LogError($"[ProductService][Delete] Database Error.");
                return ErrorResponse(ProductErrors.Product_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("Product was Sucessfully deleted.");
        }

        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"[ProductService][Get] ID was null or empty.");
                return ErrorResponse(ProductErrors.Product_Get_400_ProductID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await _productRepository.Get(id);
            if (result == null)
            {
                _logger.LogError($"[ProductService][Get] It was impossible to find a costumer with this ID.");
                return ErrorResponse(ProductErrors.Product_Get_400_ProductID_DoesnotExists.GetDescription());
            }

            return SuccessResponse(result);
        }
    }
}
