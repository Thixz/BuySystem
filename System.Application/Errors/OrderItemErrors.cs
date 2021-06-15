using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors.Costumers
{
    public enum OrderItemErrors
    {
        [Description("Order or Product references cannot be null or empty.")]
        OrderItem_Post_400_References_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Quantity.")]
        OrderItem_Post_400_ProductQuantity_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Unity Value.")]
        OrderItem_Post_400_UnityValue_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Order Item Total.")]
        OrderItem_Post_400_OrderItemTotal_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to reach database.")]
        OrderItem_Post_400_Database_Error,



        [Description("Order or Product references cannot be null or empty.")]
        OrderItem_Put_400_References_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Quantity.")]
        OrderItem_Put_400_ProductQuantity_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Unity Value.")]
        OrderItem_Put_400_UnityValue_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Order Item Total.")]
        OrderItem_Put_400_OrderItemTotal_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to reach database.")]
        OrderItem_Put_400_Database_Error,

        [Description("It was impossible to find an Order with this ID.")]
        OrderItem_Put_400_OrderID_DoesNotExists,

        [Description("ID cannot be null or empty")]
        Costumer_Put_400_OrderItemID_Cannot_Be_Null_Or_Empty,



        [Description("ID cannot be null or empty")]
        OrderItem_Delete_400_ID_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to reach database.")]
        OrderItem_Delete_400_Database_Error,

        [Description("It was impossible to find an Order with this ID.")]
        OrderItem_Delete_400_OrderID_DoesNotExists,



        [Description("ID cannot be null or empty")]
        OrderItem_Get_400_ID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find an Order with this ID.")]
        OrderItem_Get_400_OrderID_DoesNotExists,
    }
}
