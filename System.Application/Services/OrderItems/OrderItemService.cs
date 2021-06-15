using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Request.OrderItems;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Orders;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Application.Validators.OrderItems;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.OrderItems
{
    public class OrderItemService : PrepareDefault
    {
        private readonly OrderItemRepository _itemRepository;
        private readonly ILogger<OrderItemService> _logger;
        public OrderItemService(OrderItemRepository _repository, ILogger<OrderItemService> logger)
        {
            this._itemRepository = _repository;
            this._logger = logger;
        }

        public async Task<DefaultResponse> Create(OrderItemPostRequest _postRequest)
        {
            if (_postRequest.orderId == Guid.Empty || _postRequest.productId == Guid.Empty)
            {
                _logger.LogError("[OrderItemService][Create] Order or Product references cannot be null or empty.");
                return ErrorResponse(OrderItemErrors.OrderItem_Post_400_References_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new OrderItemPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[OrderService][Create] Error while trying to create order: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            var result = await _itemRepository.Create(new OrderItemEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                _logger.LogError("[OrderItemService][Create] Error while trying to reach database.");
                return ErrorResponse(OrderItemErrors.OrderItem_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Update(OrderItemPutRequest _putRequest)
        {
            if (_putRequest.id == Guid.Empty)
            {
                _logger.LogError($"[OrderItemService][Update] ID was null or empty.");
                return ErrorResponse(OrderItemErrors.Costumer_Put_400_OrderItemID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new OrderItemPutRequestValidator().Validate(_putRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[OrderService][Update] Error while trying to create order: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await _itemRepository.Get(_putRequest.id) == null)
            {
                _logger.LogError("[OrderItemService][Update] It was impossible to find an order item with this ID.");
                return ErrorResponse(OrderItemErrors.OrderItem_Put_400_OrderID_DoesNotExists.GetDescription());
            }

            var result = await _itemRepository.Update(new OrderItemEntity(_putRequest));
            if (result.Id == Guid.Empty)
                return ErrorResponse(OrderItemErrors.OrderItem_Put_400_Database_Error.GetDescription());

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("[OrderItemService][Delete] ID cannot be null or empty.");
                return ErrorResponse(OrderItemErrors.OrderItem_Delete_400_ID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await _itemRepository.Get(id) == null)
            {
                _logger.LogError("[OrderItemService][Delete] It was impossible to find an order with this ID.");
                return ErrorResponse(OrderItemErrors.OrderItem_Delete_400_OrderID_DoesNotExists.GetDescription());
            }

            if (!await _itemRepository.Delete(id))
            {
                _logger.LogError("[OrderItemService][Delete] Error while trying to reach database.");
                return ErrorResponse(OrderItemErrors.OrderItem_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("Order item was Sucessfully deleted.");
        }
        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("[OrderItemService][Get] ID cannot be null or empty.");
                return ErrorResponse(OrderItemErrors.OrderItem_Get_400_ID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await _itemRepository.Get(id);
            if (result == null)
            {
                _logger.LogError("[OrderItemService][Get] It was impossible to find an order item with this ID.");
                return ErrorResponse(OrderItemErrors.OrderItem_Get_400_OrderID_DoesNotExists.GetDescription());
            }

            return SuccessResponse(result);
        }
    }
}
