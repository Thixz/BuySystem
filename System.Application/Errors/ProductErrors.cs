using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors.Costumers
{
    public enum ProductErrors
    {
        [Description("It's necessary to inform a Product Name.")]
        Product_Post_400_ProductName_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Code.")]
        Product_Post_400_ProductCode_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Quantity.")]
        Product_Post_400_ProductQuantity_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Description.")]
        Product_Post_400_ProductDescription_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Model.")]
        Product_Post_400_ProductModel_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Value.")]
        Product_Post_400_ProductValue_Cannot_Be_Null_Or_Empty,

        [Description("A product with this code already exists.")]
        Product_Post_400_ProductCode_Cannot_Be_Duplicated,

        [Description("Error while trying to reach database.")]
        Product_Post_400_Database_Error,




        [Description("ID cannot be null or empty.")]
        Product_Put_400_ProductID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find a Product with this ID.")]
        Product_Put_400_ProductID_DoesnotExists,

        [Description("It's necessary to inform a Product Name.")]
        Product_Put_400_ProductName_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Code.")]
        Product_Put_400_ProductCode_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Quantity.")]
        Product_Put_400_ProductQuantity_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Description.")]
        Product_Put_400_ProductDescription_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Model.")]
        Product_Put_400_ProductModel_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a Product Value.")]
        Product_Put_400_ProductValue_Cannot_Be_Null_Or_Empty,

        [Description("A product with this code already exists.")]
        Product_Put_400_ProductCode_Cannot_Be_Duplicated,

        [Description("Error while trying to update product.")]
        Product_Put_400_Update_Error,



        [Description("ID cannot be null or empty.")]
        Product_Delete_400_ProductID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find a Product with this ID.")]
        Product_Delete_400_ProductID_DoesnotExists,

        [Description("Error while trying to reach database.")]
        Product_Delete_400_Database_Error,

        [Description("An order item related to this product exists..")]
        Product_Delete_400_ForeignKey_Error,



        [Description("ID cannot be null or empty")]
        Product_Get_400_ProductID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find a Product with this ID")]
        Product_Get_400_ProductID_DoesnotExists,
    }
}
