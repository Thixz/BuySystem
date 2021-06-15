using FluentValidation;
using System;
using System.Application.Contracts.Request.Adress;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.Addresses
{
    public class AddressPostRequestValidator : AbstractValidator<AdressPostRequest>
    {
        public AddressPostRequestValidator()
        {
            RuleFor(x => x.address).Must(address => !string.IsNullOrEmpty(address)).
                WithErrorCode(AddressErrors.Address_Post_400_Address_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.addressNumber).Must(addressnumber => !string.IsNullOrEmpty(addressnumber)).
                WithErrorCode(AddressErrors.Address_Post_400_AddressNumber_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.neighborhood).Must(neighborhood => !string.IsNullOrEmpty(neighborhood)).
               WithErrorCode(AddressErrors.Address_Post_400_Neighborhood_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.postalCode).Must(postalCode => !string.IsNullOrEmpty(postalCode)).
               WithErrorCode(AddressErrors.Address_Post_400_PostalCode_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.country).Must(country => !string.IsNullOrEmpty(country.ToString())).
               WithErrorCode(AddressErrors.Address_Post_400_Country_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
