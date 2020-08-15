using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Domain.Carts;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Receipts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Advertise.Core.Domain.Users
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        public virtual bool? IsBan { get; set; }
        public virtual string BannedReason { get; set; }
        public virtual string Code { get; set; }
        public virtual DateTime? BannedOn { get; set; }
        public virtual bool? IsVerify { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual bool? IsAnonymous { get; set; }
        public virtual string EmailConfirmationToken { get; set; }
        public virtual string MobileConfirmationToken { get; set; }
        public virtual DateTime? LastPasswordChangedOn { get; set; }
        public virtual DateTime? LastLoginedOn { get; set; }
        public virtual bool? IsSystemAccount { get; set; }
        public virtual string LastIp { get; set; }
        public virtual byte?[] RowVersion { get; set; }
        public bool? IsChangePermission { get; set; }
        [Column(TypeName = "xml")]
        public string DirectPermissions { get; set; }
        [NotMapped]
        public XElement XmlDirectPermissions
        {
            get { return XElement.Parse(DirectPermissions); }
            set { DirectPermissions = value.ToString(); }
        }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual bool? IsDelete { get; set; }
        public virtual int? Version { get; set; }
        public virtual UserMeta Meta { get; set; }
        public virtual Guid? MetaId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual ICollection<UserBudget> Budgets { get; set; }
        public virtual Guid? SettingId { get; set; }
        public virtual UserSetting Setting { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductCommentLike> ProductsCommentLikes { get; set; }
        public virtual ICollection<ProductLike> ProductLikes { get; set; }
        public virtual ICollection<ProductVisit> ProductVisits { get; set; }
        public virtual ICollection<CompanyFollow> CompanyFollows { get; set; }
        public virtual ICollection<CompanyQuestion> CompanyQuestions { get; set; }
        public virtual ICollection<CompanyVisit> CompanyVisits { get; set; }
        public virtual ICollection<UserViolation> Reports { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Announce> Announces { get; set; }
    }
}