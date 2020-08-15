using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
    public class ProductEditValidator : BaseValidator<ProductEditModel>
    {
        public ProductEditValidator()
        {
            RuleFor(model => model.CategoryId).NotNull().WithMessage("دسته بندی را انتخاب کنید");
            RuleFor(model => model.Price).NotNull().WithMessage("قیمت را انتخاب کنید");
            RuleFor(model => model.Sell).NotNull().WithMessage("نوع فروش را انتخاب کنید");
            //RuleFor(model => model.CompanyId).MustAsync(companyService.HasAliasAsync).WithMessage("فروشگاه شما ایجاد نشده است");
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را انتخاب کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(50).WithMessage("عنوان باید بیشتر از 2 و کمتر از 30 کاراکتر باشد");
        }
    }
}