using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionEditModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CompanyId { get; set; }
        public string CreatorFullName { get; set; }
        public string EditorFullName { get; set; }
        public Guid Id { get; set; }
        public string RejectDescription { get; set; }
        public string Title { get; set; }
    }
}