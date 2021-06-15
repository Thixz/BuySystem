using FluentValidation;
using System;
using System.Application.Contracts.Request.Costumers;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.Costumers
{
    public class CostumerPutRequestValidator : AbstractValidator<CostumerPutRequest>
    {
        public CostumerPutRequestValidator()
        {
            RuleFor(x => x.costumerName).Must(name => !string.IsNullOrEmpty(name)).
                WithErrorCode(CostumerErrors.Costumer_Put_400_CostumerName_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.document).Must(document => !string.IsNullOrEmpty(document)).
                WithErrorCode(CostumerErrors.Costumer_Put_400_Costumerdocument_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.phoneNumber).Must(phone => !string.IsNullOrEmpty(phone)).
               WithErrorCode(CostumerErrors.Costumer_Put_400_PhoneNumber_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.email).Must(email => !string.IsNullOrEmpty(email)).
               WithErrorCode(CostumerErrors.Costumer_Put_400_CostumerEmail_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.birthdate).Must(birthdate => !string.IsNullOrEmpty(birthdate.ToString())).
               WithErrorCode(CostumerErrors.Costumer_Put_400_CostumerEmail_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
