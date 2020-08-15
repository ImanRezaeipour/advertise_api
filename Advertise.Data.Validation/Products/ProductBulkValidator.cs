using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
    public class ProductBulkValidator : BaseValidator<ProductBulkModel>
    {
        public ProductBulkValidator()
        {
            RuleFor(model => model.CatalogId).NotNull().When(model => model.Id == null).WithMessage("کاتالوگ را انتخاب کنید");
            RuleFor(model => model.CategoryId).NotNull().When(model => model.Id == null).WithMessage("دسته را انتخاب کنید");
            RuleFor(model => model.Color).NotNull().When(model => model.Id == null).WithMessage("رنگ را انتخاب کنید");
            RuleFor(model => model.Price).NotNull().WithMessage("قیمت را وارد کنید");
            RuleFor(model => model.AvailableCount).NotNull().WithMessage("موجودی را وارد کنید");
        }
    }
}