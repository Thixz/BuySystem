using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors.Costumers
{
    public enum OrderErrors
    {
        [Description("ID cannot be null or empty")]
        Order_Post_400_ID_Cannot_Be_Null_Or_Empty,

        [Description("Order number cannot be duplicated")]
        Order_Post_400_OrderNumber_Cannot_Be_Duplicated,

        [Description("It's necessary to inform an Order Number")]
        Order_Post_400_OrderNumber_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Costumer Id")]
        Order_Post_400_CostumerId_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Costumer Address Id")]
        Order_Post_400_AddressId_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Order SubTotal")]
        Order_Post_400_SubTotal_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Order Total")]
        Order_Post_400_Total_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to reach database.")]
        Order_Post_400_Database_Error,




        [Description("ID cannot be null or empty")]
        Order_Delete_400_ID_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to reach database.")]
        Order_Delete_400_Database_Error,

        [Description("It was impossible to find an Order with this ID.")]
        Order_Delete_400_OrderID_DoesNotExists,

        [Description("An order item related to this order exists..")]
        Order_Delete_400_ForeignKey_Error,






        [Description("ID cannot be null or empty")]
        Order_Get_400_ID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find an Order with this ID.")]
        Order_Get_400_OrderID_DoesNotExists,
    }
}
