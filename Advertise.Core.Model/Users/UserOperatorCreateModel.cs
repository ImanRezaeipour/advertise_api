using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserOperatorCreateModel : BaseModel
    {
        public string Alias { get; set; }
        public decimal? Amount { get; set; }
        public Guid? CategoryId { get; set; }
        public string CompanyTitle { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public PaymentType PaymentType { get; set; }
        public IEnumerable<SelectList> PaymentTypeList { get; set; }
        public Guid? RoleId { get; set; }
        public IEnumerable<SelectList> RoleList { get; set; }
    }
}