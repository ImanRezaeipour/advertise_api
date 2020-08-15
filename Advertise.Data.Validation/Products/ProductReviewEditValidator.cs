using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
    public class ProductReviewEditValidator :BaseValidator<ProductReviewEditModel>
    {
        public ProductReviewEditValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("متن نقد و بررسی را وارد کنید");
        }
    }
}
