using Advertise.Core.Model.Categories;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Categories
{
    public class CategoryReviewCreateValidator : BaseValidator<CategoryReviewCreateModel>
    {
        public CategoryReviewCreateValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("نقد و بررسی را وارد کنید");
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان باید بیشتر از 2 و کمتر از 30 کاراکتر باشد ");
            RuleFor(model => model.CategoryId).NotNull().WithMessage("دسته نقد را وارد کنید");
        }
    }
}