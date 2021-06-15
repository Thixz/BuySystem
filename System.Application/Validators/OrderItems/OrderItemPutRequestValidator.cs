using FluentValidation;
using System;
using System.Application.Contracts.Request.OrderItems;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.OrderItems
{
    class OrderItemPutRequestValidator : AbstractValidator<OrderItemPutRequest>
    {
        public OrderItemPutRequestValidator()
        {
            RuleFor(x => x.quantity).Must(quantity => quantity > 0).
               WithErrorCode(OrderItemErrors.OrderItem_Put_400_ProductQuantity_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.unityValue).Must(unityValue => unityValue > 0).
                WithErrorCode(OrderItemErrors.OrderItem_Put_400_UnityValue_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.total).Must(total => total > 0).
               WithErrorCode(OrderItemErrors.OrderItem_Put_400_OrderItemTotal_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.total).Must(total => total > 0).
               WithErrorCode(OrderItemErrors.OrderItem_Put_400_OrderItemTotal_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
