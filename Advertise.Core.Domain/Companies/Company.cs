using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Companies
{
    public class Company : BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual string Title { get; set; }
        public virtual string Alias { get; set; }
        public virtual string Description { get; set; }
        public virtual string LogoFileName { get; set; }
        public virtual string CoverFileName { get; set; }
        public virtual string Color { get; set; }
        public virtual string Slogan { get; set; }
        public virtual string BackgroundFileName { get; set; }
        public virtual string ShortUrl { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string WebSite { get; set; }
        public virtual EmployeeRangeType? EmployeeRange { get; set; }
        public virtual DateTime? EstablishedOn { get; set; }
        public virtual string MetaTitle{ get; set; }
        public virtual string MetaKeywords { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual StateType? State { get; set; }
        public virtual string RejectDescription { get; set; }
        public virtual string PreviewImageFileName { get; set; }
        public virtual string ShetabNumber { get; set; }
        public virtual string ShebaNumber { get; set; }
        public virtual ClearingType? Clearing { get; set; }
        public virtual User ApprovedBy { get; set; }
        public virtual Guid? ApprovedById { get; set; }
        public virtual Location Location { get; set; }
        public virtual Guid? LocationId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Guid? CategoryId{ get; set; }
        public virtual ICollection<CompanyAttachment> Attachments { get; set; }
        public virtual ICollection<CompanyFollow> Follows { get; set; }
        public virtual ICollection<CompanyImage> Images { get; set; }
        public virtual ICollection<CompanyQuestion> Questions { get; set; }
        public virtual ICollection<CompanyReview> Reviews { get; set; }
        public virtual ICollection<CompanySocial> Socials { get; set; }
        public virtual ICollection<CompanyHour> CompanyHours { get; set; }
        public virtual ICollection<CompanyOfficial> CompanyOfficials { get; set; }
        public virtual ICollection<CompanyVisit> Visits { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CompanySlide> Slides { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}