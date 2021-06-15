using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Request.OrderItems;
using System.Application.Contracts.Request.Orders;
using System.Application.Data.Entities.OrderItems;
using System.Application.Data.Entities.Orders;
using System.Application.Data.Repositories.OrderItems;
using System.Application.Data.Repositories.Orders;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Application.Services.OrderItems;
using System.Application.Validators.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Orders
{
    public class OrderService : PrepareDefault
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemService _orderItemService;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(OrderRepository repository, OrderItemService orderItemService, ILogger<OrderService> logger , OrderItemRepository orderItemRepository)
        {
            this._orderRepository = repository;
            this._orderItemService = orderItemService;
            this._orderItemRepository = orderItemRepository;
            this._logger = logger;
        }

        public async Task<DefaultResponse> Create(OrderPostRequest _postRequest)
        {
            var validator = new OrderPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                _logger.LogError($"[OrderService][Create] Error while trying to create order: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            int errorItems = 0;
            double totalSum = 0, subTotalSum = 0, freight = 0;
            foreach (var item in _postRequest.items)
            {
                totalSum += item.total;
                freight += item.freight;
                subTotalSum += item.unityValue;
            }
            if (totalSum != _postRequest.total || freight != _postRequest.freight || subTotalSum != _postRequest.subTotal)
            {
                _logger.LogError("[OrderService][Create] Order items values didn't match with order value.");
                return ErrorResponse("Order items values didn't match with order value.");
            }

            if (await _orderRepository.GetOrderByNumber(_postRequest.orderNumber) != null)
            {
                _logger.LogError("[OrderService][Create] Order number cannot be duplicated.");
                return ErrorResponse(OrderErrors.Order_Post_400_OrderNumber_Cannot_Be_Duplicated.GetDescription());
            }

            var order = await _orderRepository.Create(new OrderEntity(_postRequest));
            if (order.Id != Guid.Empty)
            {
                foreach (var item in order.items)
                {
                    var _itemPost = new OrderItemPostRequest
                    {
                        orderId = order.Id,
                        productId = item.productId,
                        quantity = item.quantity,
                        freight = item.freight,
                        unityValue = item.unityValue,
                        total = item.total,
                    };
                    var result = await _orderItemService.Create(_itemPost);
                    if (!result.success)
                        errorItems++;

                    item.orderId = order.Id;
                }
            }
            else
            {
                _logger.LogError($"[OrderService][Update] Database Error.");
                return ErrorResponse(OrderErrors.Order_Post_400_Database_Error.GetDescription());
            }

            if (errorItems > 0)
            {
                await _orderRepository.Delete(order.Id);
                _logger.LogError("[OrderService][Create] That was an error with order items.");
                return ErrorResponse("That was an error with order items.");
            }

            return SuccessResponse(order);
        }

        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("[OrderService][Delete] ID cannot be null or empty.");
                return ErrorResponse(OrderErrors.Order_Delete_400_ID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await _orderItemRepository.GetOrderItemByOrderId(id) != null)
            {
                _logger.LogError("[OrderService][Delete] An order item related to this order exists.");
                return ErrorResponse(OrderErrors.Order_Delete_400_ForeignKey_Error.GetDescription());
            }

            if (await _orderRepository.Get(id) == null)
            {
                _logger.LogError("[OrderService][Delete] It was impossible to find an order with this ID.");
                return ErrorResponse(OrderErrors.Order_Delete_400_OrderID_DoesNotExists.GetDescription());
            }

            if (!await _orderRepository.Delete(id))
            {
                _logger.LogError("[OrderService][Delete] Error while trying to reach database.");
                return ErrorResponse(OrderErrors.Order_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("Order was Sucessfully deleted.");
        }

        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("[OrderService][Get] ID cannot be null or empty.");
                return ErrorResponse(OrderErrors.Order_Get_400_ID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await _orderRepository.Get(id);
            if (result == null)
            {
                _logger.LogError("[OrderService][Get] It was impossible to find an order with this ID.");
                return ErrorResponse(OrderErrors.Order_Get_400_OrderID_DoesNotExists.GetDescription());
            }

            result.items = new List<OrderItemEntity>();
            foreach (var item in await _orderItemRepository.GetOrderItemByOrderId(id))
            {
                result.items.Add(item);
            }

            return SuccessResponse(result);
        }
    }
}
