using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors.Costumers
{
    public enum CostumerErrors
    {
        [Description("It's necessary to inform a costumer name.")]
        Costumer_Post_400_CostumerName_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer document.")]
        Costumer_Post_400_Costumerdocument_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer phone number.")]
        Costumer_Post_400_PhoneNumber_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer email.")]
        Costumer_Post_400_CostumerEmail_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer birthdate.")]
        Costumer_Post_400_BirthDate_Cannot_Be_Null_Or_Empty,

        [Description("A customer with this document already exists.")]
        Costumer_Post_400_Document_Cannot_Be_Duplicated,

        [Description("Error while trying to reach database.")]
        Costumer_Post_400_Database_Error,


        [Description("It's necessary to inform a costumer name.")]
        Costumer_Put_400_CostumerName_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer document.")]
        Costumer_Put_400_Costumerdocument_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer phone number.")]
        Costumer_Put_400_PhoneNumber_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer email.")]
        Costumer_Put_400_CostumerEmail_Cannot_Be_Null_Or_Empty,

        [Description("It's necessary to inform a costumer birthdate.")]
        Costumer_Put_400_BirthDate_Cannot_Be_Null_Or_Empty,

        [Description("ID cannot be null or empty")]
        Costumer_Put_400_CostumerID_Cannot_Be_Null_Or_Empty,

        [Description("A customer with this document already exists.")]
        Costumer_Put_400_CostumerDocument_Cannot_Be_Duplicated,

        [Description("It was impossible to find a costumer with this ID.")]
        Costumer_Put_400_CostumerID_DoesNotExists,

        [Description("Error while trying to update costumer.")]
        Costumer_Put_400_Update_Error,



        [Description("ID cannot be null or empty")]
        Costumer_Delete_400_CostumerID_Cannot_Be_Null_Or_Empty,

        [Description("Error while trying to reach database.")]
        Costumer_Delete_400_Database_Error,

        [Description("It was impossible to find a costumer with this ID.")]
        Costumer_Delete_400_CostumerID_DoesNotExists,



        [Description("ID cannot be null or empty")]
        Costumer_Get_400_CostumerID_Cannot_Be_Null_Or_Empty,

        [Description("It was impossible to find a costumer with this ID.")]
        Costumer_Get_400_CostumerID_DoesNotExists,
    }
}
