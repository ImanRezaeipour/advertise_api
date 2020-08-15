using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
    public class ProductReviewCreateValidator :BaseValidator<ProductReviewCreateModel>
    {
        public ProductReviewCreateValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("متن نقد و بررسی را وارد کنید");
            RuleFor(model => model.ProductId).NotNull().WithMessage("محصول مورد نقد را انتخاب کنید");
        }
    }
}
