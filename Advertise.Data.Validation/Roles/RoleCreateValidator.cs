﻿using Advertise.Core.Model.Roles;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Roles
{
    public class RoleCreateValidator:BaseValidator<RoleCreateModel>
    {
        public RoleCreateValidator()
        {
            RuleFor(model => model.Name).NotNull().WithMessage("نام را انتخاب نمایید");
            //RuleFor(model => model.Name).MustAsync(async (name , token) => !await roleService.IsExistNameAsync(name,token)).WithMessage("این گروه  قبلا در سیستم ثبت شده است");
            RuleFor(model => model.Name).MinimumLength(6).MaximumLength(30).WithMessage("نام باید کمتر از 30 و بیشتر از 8 کاراکتر باشد");
        }
    }
}
