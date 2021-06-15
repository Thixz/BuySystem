using FluentValidation;
using System;
using System.Application.Contracts.Request.Orders;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.Orders
{
    public class OrderPostRequestValidator : AbstractValidator<OrderPostRequest>
    {
        public OrderPostRequestValidator()
        {
            RuleFor(x => x.orderNumber).Must(orderNumber => !string.IsNullOrEmpty(orderNumber)).
            WithErrorCode(OrderErrors.Order_Post_400_OrderNumber_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.subTotal).Must(subTotal => subTotal > 0).
                WithErrorCode(OrderErrors.Order_Post_400_SubTotal_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.total).Must(total => total > 0).
               WithErrorCode(OrderErrors.Order_Post_400_Total_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
