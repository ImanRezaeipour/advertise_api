﻿using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyReviewDetailModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CompanyId { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}