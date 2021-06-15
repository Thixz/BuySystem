using FluentValidation;
using System;
using System.Application.Contracts.Request.Products;
using System.Application.Errors.Costumers;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.Products
{
    public class ProductPostRequestValidator : AbstractValidator<ProductPostRequest>
    {
        public ProductPostRequestValidator()
        {
            RuleFor(x => x.productName).Must(productName => !string.IsNullOrEmpty(productName)).
                WithErrorCode(ProductErrors.Product_Post_400_ProductName_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.productCode).Must(productCode => !string.IsNullOrEmpty(productCode)).
                WithErrorCode(ProductErrors.Product_Post_400_ProductCode_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.quantity).Must(quantity => quantity > 0).
               WithErrorCode(ProductErrors.Product_Post_400_ProductQuantity_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.productDescription).Must(productDescription => !string.IsNullOrEmpty(productDescription)).
               WithErrorCode(ProductErrors.Product_Post_400_ProductDescription_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.model).Must(model => !string.IsNullOrEmpty(model.ToString())).
               WithErrorCode(ProductErrors.Product_Post_400_ProductModel_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.productValue).Must(productValue => productValue > 0).
              WithErrorCode(ProductErrors.Product_Post_400_ProductValue_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
