using Advertise.Core.Model.Receipts;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Receipts
{
    public class ReceiptCreateValidator:BaseValidator<ReceiptModel>
    {
        public ReceiptCreateValidator()
        {
            RuleFor(model => model.FirstName).NotNull().WithMessage("نام خود را وارد کنید");
            RuleFor(model => model.LastName).NotNull().WithMessage("نام خانوادگی خود را وارد کنید");
            RuleFor(model => model.NationalCode).NotNull().WithMessage("کد ملی را وارد کنید");
            RuleFor(model => model.NationalCode).MinimumLength(10).MaximumLength(10).WithMessage("کد ملی باید ده رقم باشد");
            RuleFor(model => model.NationalCode).Matches("^[۰-۹0-9_]*$").WithMessage("کد ملی باید عدد باشد");
            RuleFor(model => model.HomeNumber).NotNull().WithMessage("شماره تلفن ثابت را وارد کنید");
            RuleFor(model => model.HomeNumber).MinimumLength(10).MaximumLength(10).WithMessage("شماره تلفن باید 11 رقم باشد");
            RuleFor(model => model.HomeNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره تلفن باید عدد باشد");
            RuleFor(model => model.PhoneNumber).NotNull().WithMessage("شماره همراه را وارد کنید");
            RuleFor(model => model.PhoneNumber).MinimumLength(10).MaximumLength(10).WithMessage("شماره همراه باید 11 رقم باشد");
            RuleFor(model => model.PhoneNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره همراه باید عدد باشد");
            RuleFor(model => model.Payment).NotNull().WithMessage("نوع پرداخت را انتخاب نمائید");
            RuleFor(model => model.Location.LocationCity.Id).NotNull().WithMessage("لطفا استان محل سکونت خود را تعیین نمایید");
            RuleFor(model => model.Location.Extra).NotNull().WithMessage("لطفا نشانی خود را وارد نمایید");
            RuleFor(model => model.Location.LocationCity.ParentId).NotNull().WithMessage("لطفا استان محل سکونت خود را تعیین نمایید");
        }
    }
}
