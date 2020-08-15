using Advertise.Core.Model.Notices;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Notices
{
    public class NoticeCreateValidator:BaseValidator<NoticeCreateModel>
    {
        public NoticeCreateValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("متن را وارد کنید");
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان باید بیشتر از 2 و کمتر از 30 ;hvh;jv fhan");
        }
    }
}
