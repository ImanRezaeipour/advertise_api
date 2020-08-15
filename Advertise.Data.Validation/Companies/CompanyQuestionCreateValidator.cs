using Advertise.Core.Model.Companies;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Companies
{
    public class CompanyQuestionCreateValidator:BaseValidator<CompanyQuestionCreateModel>
    {
        public CompanyQuestionCreateValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("لطفا سوال خود را بنویسید");
            RuleFor(model => model.Body).MinimumLength(3).MaximumLength(200).WithMessage("سوال باید بیشتر از 3 و کمتر از 200 کاراکتر باشد");
        }
    }
}