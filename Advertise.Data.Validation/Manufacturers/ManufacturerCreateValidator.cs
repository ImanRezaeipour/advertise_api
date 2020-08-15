﻿using Advertise.Core.Model.Manufacturers;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Manufacturers
{
    public class ManufacturerCreateValidator:BaseValidator<ManufacturerCreateModel>
    {
        public ManufacturerCreateValidator()
        {
            RuleFor(model => model.Country).NotNull().WithMessage("لطفاکشور را انتخاب کنید");
            RuleFor(model => model.Name).NotNull().WithMessage("لطفا نام برند را وارد کنید");
        }
    }
}
