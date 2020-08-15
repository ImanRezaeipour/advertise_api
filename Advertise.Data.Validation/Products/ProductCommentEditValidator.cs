using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
   public class ProductCommentEditValidator:BaseValidator<ProductCommentEditModel>
    {
        public ProductCommentEditValidator()
        {
            RuleFor(model => model.Body).NotNull().MaximumLength(2).MaximumLength(200).WithMessage("متن را وارد کنید");
            RuleFor(model => model.ProductId).NotNull().WithMessage("محصول نامشخص است");
        }
    }
}
