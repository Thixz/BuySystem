using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors.Costumers
{
    public enum AddressErrors
    {
        [Description("It's necessary to inform a costumer Id")]
        Address_Post_400_CostumerId_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address")]
        Address_Post_400_Address_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Number")]
        Address_Post_400_AddressNumber_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Neighborhood")]
        Address_Post_400_Neighborhood_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Postal Code")]
        Address_Post_400_PostalCode_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Country")]
        Address_Post_400_Country_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to create Address")]
        Address_Post_400_Create_Error,



        [Description("ID cannot be null or empty")]
        Address_Put_400_AddressID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find an Adrress with this ID")]
        Address_Put_400_AddressID_DoesnotExists,

        [Description("It's necessary to inform a costumer Id")]
        Address_Put_400_CostumerId_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address")]
        Address_Put_400_Address_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Number")]
        Address_Put_400_AddressNumber_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Neighborhood")]
        Address_Put_400_Neighborhood_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Postal Code")]
        Address_Put_400_PostalCode_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform an Address Country")]
        Address_Put_400_Country_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to Update Address")]
        Address_Put_400_Update_Error,



        [Description("ID cannot be null or empty")]
        Address_Delete_400_AddressID_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to reach database.")]
        Address_Delete_400_Database_Error,

        [Description("It was impossible to find an Address with this ID.")]
        Address_Delete_400_AddressID_DoesNotExists,

        [Description("An order item related to this product exists..")]
        Address_Delete_400_ForeignKey_Error,



        [Description("ID cannot be null or empty")]
        Address_Get_400_AddressID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find a costumer with this ID.")]
        Address_Get_400_AddressID_DoesNotExists,

        [Description("Error while trying to Get Address")]
        Address_Post_400_Get_Error,
    }
}
