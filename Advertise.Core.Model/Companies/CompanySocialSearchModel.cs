﻿using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySocialSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
    }
}