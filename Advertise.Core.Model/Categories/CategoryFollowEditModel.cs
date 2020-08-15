using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryFollowEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsFollow { get; set; }
    }
}