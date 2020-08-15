﻿using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductFeatureCreateModel : BaseModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
    }
}