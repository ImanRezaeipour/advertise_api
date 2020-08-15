using Advertise.Core.Model.Categories;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Categories
{
    public class CategoryEditValidator : BaseValidator<CategoryEditModel>
    {
        public CategoryEditValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.MetaTitle).NotNull().WithMessage("عنوان مستعار را وارد کنید");
            RuleFor(model => model.Order).NotNull().WithMessage("الویت را وارد کنید");
            RuleFor(model => model.Description).MaximumLength(1000000).WithMessage("تعداد کاراکتر وارد شده بیش از حد مجاز است");
            RuleFor(model => model.ParentId).NotNull().WithMessage("پدر دسته را وارد کنید");
            RuleFor(model => model.Alias).NotNull().WithMessage("نام مستعار دسته را وارد کنید");
            RuleFor(model => model.Alias).Matches("^([A-Za-z]{1}[A-Za-z-]{1,}[A-Za-z]{1,})$").WithMessage("شما می‌بایست تنها از حروف الفبای انگلیسی و خط‌فاصله استفاده نمایید که این نام الزاماً با حروف الفبای انگلیسی آغاز می‌شود و حرف آخر نمی‌تواند خط ‌فاصله باشد");
        }
    }
}