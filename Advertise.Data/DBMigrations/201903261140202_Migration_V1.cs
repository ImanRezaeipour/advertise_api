namespace Advertise.Data.DbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AD_Roles",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        Role_Code = c.String(),
                        Role_IsSystemRole = c.Boolean(),
                        Role_IsBan = c.Boolean(),
                        Role_Permissions = c.String(storeType: "xml"),
                        Role_CreatedOn = c.DateTime(),
                        Role_ModifiedOn = c.DateTime(),
                        Role_CreatorIp = c.String(),
                        Role_ModifierIp = c.String(),
                        Role_IsModifyLock = c.Boolean(),
                        Role_IsDelete = c.Boolean(),
                        Role_ModifierAgent = c.String(),
                        Role_CreatorAgent = c.String(),
                        Role_Version = c.Int(),
                        Role_Audit = c.Int(),
                        Role_CreatedById = c.Guid(),
                        Role_Name = c.String(),
                    })
                .PrimaryKey(t => t.Role_Id)
                .ForeignKey("dbo.AD_Users", t => t.Role_CreatedById)
                .Index(t => t.Role_CreatedById);
            
            CreateTable(
                "dbo.AD_Users",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        User_IsBan = c.Boolean(),
                        User_BannedReason = c.String(),
                        User_Code = c.String(),
                        User_BannedOn = c.DateTime(),
                        User_IsVerify = c.Boolean(),
                        User_IsActive = c.Boolean(),
                        User_IsAnonymous = c.Boolean(),
                        User_EmailConfirmationToken = c.String(),
                        User_MobileConfirmationToken = c.String(),
                        User_LastPasswordChangedOn = c.DateTime(),
                        User_LastLoginedOn = c.DateTime(),
                        User_IsSystemAccount = c.Boolean(),
                        User_LastIp = c.String(),
                        User_IsChangePermission = c.Boolean(),
                        User_DirectPermissions = c.String(storeType: "xml"),
                        User_CreatedOn = c.DateTime(),
                        User_IsDelete = c.Boolean(),
                        User_Version = c.Int(),
                        User_MetaId = c.Guid(),
                        User_CompanyId = c.Guid(),
                        User_CreatedById = c.Guid(nullable: false),
                        User_Email = c.String(),
                        User_EmailConfirmed = c.Boolean(nullable: false),
                        User_PasswordHash = c.String(),
                        User_SecurityStamp = c.String(),
                        User_PhoneNumber = c.String(),
                        User_PhoneNumberConfirmed = c.Boolean(nullable: false),
                        User_TwoFactorEnabled = c.Boolean(nullable: false),
                        User_LockoutEndDateUtc = c.DateTime(),
                        User_LockoutEnabled = c.Boolean(nullable: false),
                        User_AccessFailedCount = c.Int(nullable: false),
                        User_UserName = c.String(),
                    })
                .PrimaryKey(t => t.User_Id)
                .ForeignKey("dbo.AD_Companies", t => t.User_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.User_CreatedById)
                .ForeignKey("dbo.AD_UserMetas", t => t.User_MetaId)
                .Index(t => t.User_MetaId)
                .Index(t => t.User_CompanyId)
                .Index(t => t.User_CreatedById);
            
            CreateTable(
                "dbo.AD_Announces",
                c => new
                    {
                        Announce_Id = c.Guid(nullable: false),
                        Announce_Title = c.String(),
                        Announce_ImageFileName = c.String(),
                        Announce_EntityName = c.String(),
                        Announce_EntityId = c.Guid(),
                        Announce_TargetId = c.Guid(),
                        Announce_IsApprove = c.Boolean(),
                        Announce_DurationType = c.Int(),
                        Announce_FinalPrice = c.Decimal(precision: 18, scale: 2),
                        Announce_Order = c.Int(),
                        Announce_OwnerId = c.Guid(),
                        Announce_AnnounceOptionId = c.Guid(),
                        Announce_CategoryId = c.Guid(),
                        Announce_CreatedOn = c.DateTime(),
                        Announce_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Announce_Id)
                .ForeignKey("dbo.AD_AnnounceOptions", t => t.Announce_AnnounceOptionId)
                .ForeignKey("dbo.AD_Categories", t => t.Announce_CategoryId)
                .ForeignKey("dbo.AD_Users", t => t.Announce_OwnerId)
                .Index(t => t.Announce_OwnerId)
                .Index(t => t.Announce_AnnounceOptionId)
                .Index(t => t.Announce_CategoryId);
            
            CreateTable(
                "dbo.AD_AnnounceOptions",
                c => new
                    {
                        AnnounceOption_Id = c.Guid(nullable: false),
                        AnnounceOption_Title = c.String(),
                        AnnounceOption_Capacity = c.Int(),
                        AnnounceOption_Price = c.Decimal(precision: 18, scale: 2),
                        AnnounceOption_PositionType = c.Int(),
                        AnnounceOption_Type = c.Int(),
                        AnnounceOption_CreatedOn = c.DateTime(),
                        AnnounceOption_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.AnnounceOption_Id);
            
            CreateTable(
                "dbo.AD_Categories",
                c => new
                    {
                        Category_Id = c.Guid(nullable: false),
                        Category_Code = c.String(),
                        Category_Alias = c.String(),
                        Category_Order = c.Int(nullable: false),
                        Category_Title = c.String(),
                        Category_Description = c.String(),
                        Category_ImageFileName = c.String(),
                        Category_HasChild = c.Boolean(),
                        Category_IsActive = c.Boolean(),
                        Category_Type = c.Int(),
                        Category_MetaTitle = c.String(),
                        Category_MetaKeywords = c.String(),
                        Category_MetaDescription = c.String(),
                        Category_Level = c.Int(),
                        Category_ParentId = c.Guid(),
                        Category_CategoryOptionId = c.Guid(),
                        Category_CreatedById = c.Guid(),
                        Category_CreatedOn = c.DateTime(),
                        Category_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Category_Id)
                .ForeignKey("dbo.AD_CategoryOptions", t => t.Category_CategoryOptionId)
                .ForeignKey("dbo.AD_Categories", t => t.Category_ParentId)
                .ForeignKey("dbo.AD_Users", t => t.Category_CreatedById)
                .Index(t => t.Category_ParentId)
                .Index(t => t.Category_CategoryOptionId)
                .Index(t => t.Category_CreatedById);
            
            CreateTable(
                "dbo.AD_CategoryOptions",
                c => new
                    {
                        CategoryOption_Id = c.Guid(nullable: false),
                        CategoryOption_Title = c.String(),
                        CategoryOption_ProductText = c.String(),
                        CategoryOption_ProductDescriptionText = c.String(),
                        CategoryOption_ProductsManagementText = c.String(),
                        CategoryOption_CompanyText = c.String(),
                        CategoryOption_CompanyInfoText = c.String(),
                        CategoryOption_CompanyManagementText = c.String(),
                        CategoryOption_MyCompanyText = c.String(),
                        CategoryOption_HasPrice = c.Boolean(),
                        CategoryOption_BackgroundFileName = c.String(),
                        CategoryOption_CreatedOn = c.DateTime(),
                        CategoryOption_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryOption_Id);
            
            CreateTable(
                "dbo.AD_Products",
                c => new
                    {
                        Product_Id = c.Guid(nullable: false),
                        Product_Code = c.String(),
                        Product_Title = c.String(),
                        Product_IsSecondHand = c.Boolean(),
                        Product_SecondHandCode = c.String(),
                        Product_Price = c.Decimal(precision: 18, scale: 2),
                        Product_Description = c.String(),
                        Product_Body = c.String(),
                        Product_MobileNumber = c.String(),
                        Product_PhoneNumber = c.String(),
                        Product_Email = c.String(),
                        Product_IsAllowComment = c.Boolean(),
                        Product_IsAllowCommentForAnonymous = c.Boolean(),
                        Product_IsEnableForShare = c.Boolean(),
                        Product_State = c.Int(),
                        Product_Sell = c.Int(),
                        Product_RejectDescription = c.String(),
                        Product_MetaTitle = c.String(),
                        Product_MetaKeywords = c.String(),
                        Product_MetaDescription = c.String(),
                        Product_ImageFileName = c.String(),
                        Product_DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        Product_PreviousPrice = c.Decimal(precision: 18, scale: 2),
                        Product_IsCatalog = c.Boolean(),
                        Product_Color = c.Int(),
                        Product_AvailableCount = c.Int(),
                        Product_ApprovedById = c.Guid(),
                        Product_ModifiedOn = c.DateTime(),
                        Product_LocationId = c.Guid(),
                        Product_CategoryId = c.Guid(),
                        Product_CompanyId = c.Guid(),
                        Product_CreatedById = c.Guid(),
                        Product_CatalogId = c.Guid(),
                        Product_GuaranteeId = c.Guid(),
                        Product_GuaranteeTitle = c.String(),
                        Product_ManufacturerId = c.Guid(),
                        Product_CreatedOn = c.DateTime(),
                        Product_IsDelete = c.Boolean(),
                        CategoryOption_Id = c.Guid(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Product_Id)
                .ForeignKey("dbo.AD_Users", t => t.Product_ApprovedById)
                .ForeignKey("dbo.AD_Catalogs", t => t.Product_CatalogId)
                .ForeignKey("dbo.AD_Categories", t => t.Product_CategoryId)
                .ForeignKey("dbo.AD_Companies", t => t.Product_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.Product_CreatedById)
                .ForeignKey("dbo.AD_Guarantees", t => t.Product_GuaranteeId)
                .ForeignKey("dbo.AD_Locations", t => t.Product_LocationId)
                .ForeignKey("dbo.AD_Manufacturers", t => t.Product_ManufacturerId)
                .ForeignKey("dbo.AD_CategoryOptions", t => t.CategoryOption_Id)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.Product_ApprovedById)
                .Index(t => t.Product_LocationId)
                .Index(t => t.Product_CategoryId)
                .Index(t => t.Product_CompanyId)
                .Index(t => t.Product_CreatedById)
                .Index(t => t.Product_CatalogId)
                .Index(t => t.Product_GuaranteeId)
                .Index(t => t.Product_ManufacturerId)
                .Index(t => t.CategoryOption_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_Carts",
                c => new
                    {
                        Cart_Id = c.Guid(nullable: false),
                        Cart_ProductCount = c.Int(),
                        Cart_IsOrder = c.Boolean(),
                        Cart_IsCancel = c.Boolean(),
                        Cart_ProductId = c.Guid(),
                        Cart_CreatedById = c.Guid(),
                        Cart_CreatedOn = c.DateTime(),
                        Cart_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Cart_Id)
                .ForeignKey("dbo.AD_Users", t => t.Cart_CreatedById)
                .ForeignKey("dbo.AD_Products", t => t.Cart_ProductId)
                .Index(t => t.Cart_ProductId)
                .Index(t => t.Cart_CreatedById);
            
            CreateTable(
                "dbo.AD_Catalogs",
                c => new
                    {
                        Catalog_Id = c.Guid(nullable: false),
                        Catalog_Body = c.String(),
                        Catalog_CategoryId = c.Guid(),
                        Catalog_Code = c.String(),
                        Catalog_Description = c.String(),
                        Catalog_ManufacturerId = c.Guid(),
                        Catalog_MetaDescription = c.String(),
                        Catalog_MetaKeywords = c.String(),
                        Catalog_MetaTitle = c.String(),
                        Catalog_CreatedById = c.Guid(),
                        Catalog_Title = c.String(),
                        Catalog_ImageFileName = c.String(),
                        Catalog_CreatedOn = c.DateTime(),
                        Catalog_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Catalog_Id)
                .ForeignKey("dbo.AD_Categories", t => t.Catalog_CategoryId)
                .ForeignKey("dbo.AD_Users", t => t.Catalog_CreatedById)
                .ForeignKey("dbo.AD_Manufacturers", t => t.Catalog_ManufacturerId)
                .Index(t => t.Catalog_CategoryId)
                .Index(t => t.Catalog_ManufacturerId)
                .Index(t => t.Catalog_CreatedById);
            
            CreateTable(
                "dbo.AD_CatalogFeatures",
                c => new
                    {
                        CatalogFeature_Id = c.Guid(nullable: false),
                        CatalogFeature_Title = c.String(),
                        CatalogFeature_CatalogId = c.Guid(),
                        CatalogFeature_CreatedOn = c.DateTime(),
                        CatalogFeature_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CatalogFeature_Id)
                .ForeignKey("dbo.AD_Catalogs", t => t.CatalogFeature_CatalogId)
                .Index(t => t.CatalogFeature_CatalogId);
            
            CreateTable(
                "dbo.AD_CatalogImages",
                c => new
                    {
                        CatalogImage_Id = c.Guid(nullable: false),
                        CatalogImage_Order = c.Int(),
                        CatalogImage_IsWatermark = c.Boolean(),
                        CatalogImage_CatalogId = c.Guid(),
                        CatalogImage_Title = c.String(),
                        CatalogImage_FileName = c.String(),
                        CatalogImage_FileSize = c.String(),
                        CatalogImage_FileExtension = c.String(),
                        CatalogImage_FileDimension = c.String(),
                        CatalogImage_CreatedOn = c.DateTime(),
                        CatalogImage_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CatalogImage_Id)
                .ForeignKey("dbo.AD_Catalogs", t => t.CatalogImage_CatalogId)
                .Index(t => t.CatalogImage_CatalogId);
            
            CreateTable(
                "dbo.AD_CatalogKeywords",
                c => new
                    {
                        CatalogKeyword_Id = c.Guid(nullable: false),
                        CatalogKeyword_Title = c.String(),
                        CatalogKeyword_KeywordId = c.Guid(),
                        CatalogKeyword_CatalogId = c.Guid(),
                        CatalogKeyword_CreatedOn = c.DateTime(),
                        CatalogKeyword_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CatalogKeyword_Id)
                .ForeignKey("dbo.AD_Catalogs", t => t.CatalogKeyword_CatalogId)
                .ForeignKey("dbo.AD_Keywords", t => t.CatalogKeyword_KeywordId)
                .Index(t => t.CatalogKeyword_KeywordId)
                .Index(t => t.CatalogKeyword_CatalogId);
            
            CreateTable(
                "dbo.AD_Keywords",
                c => new
                    {
                        Keyword_Id = c.Guid(nullable: false),
                        Keyword_Title = c.String(),
                        Keyword_IsActive = c.Boolean(nullable: false),
                        Keyword_CreatedById = c.Guid(),
                        Keyword_CreatedOn = c.DateTime(),
                        Keyword_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Keyword_Id)
                .ForeignKey("dbo.AD_Users", t => t.Keyword_CreatedById)
                .Index(t => t.Keyword_CreatedById);
            
            CreateTable(
                "dbo.AD_ProductKeywords",
                c => new
                    {
                        ProductKeyword_Id = c.Guid(nullable: false),
                        ProductKeyword_Title = c.String(),
                        ProductKeyword_KeywordId = c.Guid(),
                        ProductKeyword_ProductId = c.Guid(),
                        ProductKeyword_CreatedOn = c.DateTime(),
                        ProductKeyword_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductKeyword_Id)
                .ForeignKey("dbo.AD_Keywords", t => t.ProductKeyword_KeywordId)
                .ForeignKey("dbo.AD_Products", t => t.ProductKeyword_ProductId)
                .Index(t => t.ProductKeyword_KeywordId)
                .Index(t => t.ProductKeyword_ProductId);
            
            CreateTable(
                "dbo.AD_CatalogLikes",
                c => new
                    {
                        CatalogLike_Id = c.Guid(nullable: false),
                        CatalogLike_IsLike = c.Boolean(),
                        CatalogLike_LikedById = c.Guid(),
                        CatalogLike_CatalogId = c.Guid(),
                        CatalogLike_CreatedOn = c.DateTime(),
                        CatalogLike_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CatalogLike_Id)
                .ForeignKey("dbo.AD_Catalogs", t => t.CatalogLike_CatalogId)
                .ForeignKey("dbo.AD_Users", t => t.CatalogLike_LikedById)
                .Index(t => t.CatalogLike_LikedById)
                .Index(t => t.CatalogLike_CatalogId);
            
            CreateTable(
                "dbo.AD_Manufacturers",
                c => new
                    {
                        Manufacturer_Id = c.Guid(nullable: false),
                        Manufacturer_Country = c.Int(),
                        Manufacturer_Name = c.String(),
                        Manufacturer_CreatedOn = c.DateTime(),
                        Manufacturer_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Manufacturer_Id);
            
            CreateTable(
                "dbo.AD_CatalogReviews",
                c => new
                    {
                        CatalogReview_Id = c.Guid(nullable: false),
                        CatalogReview_CatalogId = c.Guid(),
                        CatalogReview_Body = c.String(),
                        CatalogReview_Title = c.String(),
                        CatalogReview_IsActive = c.Boolean(),
                        CatalogReview_CreatedOn = c.DateTime(),
                        CatalogReview_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CatalogReview_Id)
                .ForeignKey("dbo.AD_Catalogs", t => t.CatalogReview_CatalogId)
                .Index(t => t.CatalogReview_CatalogId);
            
            CreateTable(
                "dbo.AD_CatalogSpecifications",
                c => new
                    {
                        CatalogSpecification_Id = c.Guid(nullable: false),
                        CatalogSpecification_Value = c.String(),
                        CatalogSpecification_CatalogId = c.Guid(),
                        CatalogSpecification_SpecificationId = c.Guid(),
                        CatalogSpecification_SpecificationOptionId = c.Guid(),
                        CatalogSpecification_CreatedOn = c.DateTime(),
                        CatalogSpecification_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CatalogSpecification_Id)
                .ForeignKey("dbo.AD_Catalogs", t => t.CatalogSpecification_CatalogId)
                .ForeignKey("dbo.AD_Specifications", t => t.CatalogSpecification_SpecificationId)
                .ForeignKey("dbo.AD_SpecificationOptions", t => t.CatalogSpecification_SpecificationOptionId)
                .Index(t => t.CatalogSpecification_CatalogId)
                .Index(t => t.CatalogSpecification_SpecificationId)
                .Index(t => t.CatalogSpecification_SpecificationOptionId);
            
            CreateTable(
                "dbo.AD_Specifications",
                c => new
                    {
                        Specification_Id = c.Guid(nullable: false),
                        Specification_Title = c.String(),
                        Specification_Type = c.Int(),
                        Specification_Description = c.String(),
                        Specification_Order = c.Int(),
                        Specification_CategoryId = c.Guid(),
                        Specification_IsSearchable = c.Boolean(),
                        Specification_CreatedOn = c.DateTime(),
                        Specification_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Specification_Id)
                .ForeignKey("dbo.AD_Categories", t => t.Specification_CategoryId)
                .Index(t => t.Specification_CategoryId);
            
            CreateTable(
                "dbo.AD_SpecificationOptions",
                c => new
                    {
                        SpecificationOption_Id = c.Guid(nullable: false),
                        SpecificationOption_Title = c.String(),
                        SpecificationOption_Description = c.String(),
                        SpecificationOption_SpecificationId = c.Guid(),
                        SpecificationOption_CreatedOn = c.DateTime(),
                        SpecificationOption_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.SpecificationOption_Id)
                .ForeignKey("dbo.AD_Specifications", t => t.SpecificationOption_SpecificationId)
                .Index(t => t.SpecificationOption_SpecificationId);
            
            CreateTable(
                "dbo.AD_ProductSpecifications",
                c => new
                    {
                        ProductSpecification_Id = c.Guid(nullable: false),
                        ProductSpecification_Value = c.String(),
                        ProductSpecification_ProductId = c.Guid(),
                        ProductSpecification_SpecificationId = c.Guid(),
                        ProductSpecification_SpecificationOptionId = c.Guid(),
                        ProductSpecification_CreatedOn = c.DateTime(),
                        ProductSpecification_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductSpecification_Id)
                .ForeignKey("dbo.AD_Products", t => t.ProductSpecification_ProductId)
                .ForeignKey("dbo.AD_Specifications", t => t.ProductSpecification_SpecificationId)
                .ForeignKey("dbo.AD_SpecificationOptions", t => t.ProductSpecification_SpecificationOptionId)
                .Index(t => t.ProductSpecification_ProductId)
                .Index(t => t.ProductSpecification_SpecificationId)
                .Index(t => t.ProductSpecification_SpecificationOptionId);
            
            CreateTable(
                "dbo.AD_ProductComments",
                c => new
                    {
                        ProductComment_Id = c.Guid(nullable: false),
                        ProductComment_Body = c.String(),
                        ProductComment_State = c.Int(),
                        ProductComment_RejectDescription = c.String(),
                        ProductComment_CommentedById = c.Guid(),
                        ProductComment_ApprovedById = c.Guid(),
                        ProductComment_ProductId = c.Guid(),
                        ProductComment_CreatedOn = c.DateTime(),
                        ProductComment_IsDelete = c.Boolean(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.ProductComment_Id)
                .ForeignKey("dbo.AD_Users", t => t.ProductComment_ApprovedById)
                .ForeignKey("dbo.AD_Users", t => t.ProductComment_CommentedById)
                .ForeignKey("dbo.AD_Products", t => t.ProductComment_ProductId)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.ProductComment_CommentedById)
                .Index(t => t.ProductComment_ApprovedById)
                .Index(t => t.ProductComment_ProductId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_Companies",
                c => new
                    {
                        Company_Id = c.Guid(nullable: false),
                        Company_Code = c.String(),
                        Company_Title = c.String(),
                        Company_Alias = c.String(),
                        Company_Description = c.String(),
                        Company_LogoFileName = c.String(),
                        Company_CoverFileName = c.String(),
                        Company_Color = c.String(),
                        Company_Slogan = c.String(),
                        Company_BackgroundFileName = c.String(),
                        Company_ShortUrl = c.String(),
                        Company_PhoneNumber = c.String(),
                        Company_MobileNumber = c.String(),
                        Company_Email = c.String(),
                        Company_WebSite = c.String(),
                        Company_EmployeeRange = c.Int(),
                        Company_EstablishedOn = c.DateTime(),
                        Company_MetaTitle = c.String(),
                        Company_MetaKeywords = c.String(),
                        Company_MetaDescription = c.String(),
                        Company_State = c.Int(),
                        Company_RejectDescription = c.String(),
                        Company_PreviewImageFileName = c.String(),
                        Company_ShetabNumber = c.String(),
                        Company_ShebaNumber = c.String(),
                        Company_Clearing = c.Int(),
                        Company_ApprovedById = c.Guid(),
                        Company_LocationId = c.Guid(),
                        Company_CategoryId = c.Guid(),
                        Company_CreatedById = c.Guid(),
                        Company_CreatedOn = c.DateTime(),
                        Company_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Company_Id)
                .ForeignKey("dbo.AD_Users", t => t.Company_ApprovedById)
                .ForeignKey("dbo.AD_Categories", t => t.Company_CategoryId)
                .ForeignKey("dbo.AD_Users", t => t.Company_CreatedById)
                .ForeignKey("dbo.AD_Locations", t => t.Company_LocationId)
                .Index(t => t.Company_ApprovedById)
                .Index(t => t.Company_LocationId)
                .Index(t => t.Company_CategoryId)
                .Index(t => t.Company_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyAttachments",
                c => new
                    {
                        CompanyAttachment_Id = c.Guid(nullable: false),
                        CompanyAttachment_Title = c.String(),
                        CompanyAttachment_Description = c.String(),
                        CompanyAttachment_Order = c.Int(),
                        CompanyAttachment_Type = c.Int(),
                        CompanyAttachment_State = c.Int(),
                        CompanyAttachment_RejectDescription = c.String(),
                        CompanyAttachment_CompanyId = c.Guid(),
                        CompanyAttachment_ApprovedById = c.Guid(),
                        CompanyAttachment_CreatedById = c.Guid(),
                        CompanyAttachment_CreatedOn = c.DateTime(),
                        CompanyAttachment_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyAttachment_Id)
                .ForeignKey("dbo.AD_Users", t => t.CompanyAttachment_ApprovedById)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyAttachment_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyAttachment_CreatedById)
                .Index(t => t.CompanyAttachment_CompanyId)
                .Index(t => t.CompanyAttachment_ApprovedById)
                .Index(t => t.CompanyAttachment_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyAttachmentFiles",
                c => new
                    {
                        CompanyAttachmentFile_Id = c.Guid(nullable: false),
                        CompanyAttachmentFile_Order = c.Int(),
                        CompanyAttachmentFile_CompanyAttachmentId = c.Guid(),
                        CompanyAttachmentFile_FileName = c.String(),
                        CompanyAttachmentFile_FileSize = c.String(),
                        CompanyAttachmentFile_FileExtension = c.String(),
                        CompanyAttachmentFile_FileDimension = c.String(),
                        CompanyAttachmentFile_CreatedOn = c.DateTime(),
                        CompanyAttachmentFile_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyAttachmentFile_Id)
                .ForeignKey("dbo.AD_CompanyAttachments", t => t.CompanyAttachmentFile_CompanyAttachmentId)
                .Index(t => t.CompanyAttachmentFile_CompanyAttachmentId);
            
            CreateTable(
                "dbo.AD_CompanyHours",
                c => new
                    {
                        CompanyHour_Id = c.Guid(nullable: false),
                        CompanyHour_DayOfWeek = c.Int(),
                        CompanyHour_EndedOn = c.Time(precision: 7),
                        CompanyHour_StartedOn = c.Time(precision: 7),
                        CompanyHour_IsActive = c.Boolean(),
                        CompanyHour_CompanyId = c.Guid(),
                        CompanyHour_CreatedById = c.Guid(),
                        CompanyHour_CreatedOn = c.DateTime(),
                        CompanyHour_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyHour_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyHour_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyHour_CreatedById)
                .Index(t => t.CompanyHour_CompanyId)
                .Index(t => t.CompanyHour_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyOfficials",
                c => new
                    {
                        CompanyOfficial_Id = c.Guid(nullable: false),
                        CompanyOfficial_BusinessLicenseFileName = c.String(),
                        CompanyOfficial_IsApprove = c.Boolean(),
                        CompanyOfficial_IsComplete = c.Boolean(nullable: false),
                        CompanyOfficial_NationalCardFileName = c.String(),
                        CompanyOfficial_OfficialNewspaperAddress = c.String(),
                        CompanyOfficial_CompanyId = c.Guid(),
                        CompanyOfficial_CreatedOn = c.DateTime(),
                        CompanyOfficial_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyOfficial_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyOfficial_CompanyId)
                .Index(t => t.CompanyOfficial_CompanyId);
            
            CreateTable(
                "dbo.AD_CompanyFollows",
                c => new
                    {
                        CompanyFollow_Id = c.Guid(nullable: false),
                        CompanyFollow_FollowedById = c.Guid(),
                        CompanyFollow_CompanyId = c.Guid(),
                        CompanyFollow_IsFollow = c.Boolean(),
                        CompanyFollow_CreatedOn = c.DateTime(),
                        CompanyFollow_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyFollow_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyFollow_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyFollow_FollowedById)
                .Index(t => t.CompanyFollow_FollowedById)
                .Index(t => t.CompanyFollow_CompanyId);
            
            CreateTable(
                "dbo.AD_CompanyImages",
                c => new
                    {
                        CompanyImage_Id = c.Guid(nullable: false),
                        CompanyImage_Order = c.Int(),
                        CompanyImage_State = c.Int(),
                        CompanyImage_Title = c.String(),
                        CompanyImage_RejectDescription = c.String(),
                        CompanyImage_CompanyId = c.Guid(),
                        CompanyImage_ApprovedById = c.Guid(),
                        CompanyImage_CreatedById = c.Guid(),
                        CompanyImage_CreatedOn = c.DateTime(),
                        CompanyImage_IsDelete = c.Boolean(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.CompanyImage_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyImage_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyImage_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.CompanyImage_CompanyId)
                .Index(t => t.CompanyImage_CreatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_CompanyImageFiles",
                c => new
                    {
                        CompanyImageFile_Id = c.Guid(nullable: false),
                        CompanyImageFile_Order = c.Int(),
                        CompanyImageFile_IsWatermark = c.Boolean(),
                        CompanyImageFile_CompanyImageId = c.Guid(),
                        CompanyImageFile_Title = c.String(),
                        CompanyImageFile_FileName = c.String(),
                        CompanyImageFile_FileSize = c.String(),
                        CompanyImageFile_FileExtension = c.String(),
                        CompanyImageFile_FileDimension = c.String(),
                        CompanyImageFile_CreatedOn = c.DateTime(),
                        CompanyImageFile_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyImageFile_Id)
                .ForeignKey("dbo.AD_CompanyImages", t => t.CompanyImageFile_CompanyImageId)
                .Index(t => t.CompanyImageFile_CompanyImageId);
            
            CreateTable(
                "dbo.AD_Locations",
                c => new
                    {
                        Location_Id = c.Guid(nullable: false),
                        Location_Latitude = c.String(),
                        Location_Longitude = c.String(),
                        Location_Street = c.String(),
                        Location_Extra = c.String(),
                        Location_PostalCode = c.String(),
                        Location_CityId = c.Guid(),
                        Location_CreatedById = c.Guid(),
                        Location_CreatedOn = c.DateTime(),
                        Location_IsDelete = c.Boolean(),
                        LocationCity_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Location_Id)
                .ForeignKey("dbo.AD_Users", t => t.Location_CreatedById)
                .ForeignKey("dbo.AD_LocationCities", t => t.LocationCity_Id)
                .Index(t => t.Location_CreatedById)
                .Index(t => t.LocationCity_Id);
            
            CreateTable(
                "dbo.AD_LocationCities",
                c => new
                    {
                        LocationCity_Id = c.Guid(nullable: false),
                        LocationCity_IsState = c.Boolean(),
                        LocationCity_Name = c.String(),
                        LocationCity_IsActive = c.Boolean(),
                        LocationCity_Latitude = c.String(),
                        LocationCity_Longitude = c.String(),
                        LocationCity_ParentId = c.Guid(),
                        LocationCity_CreatedOn = c.DateTime(),
                        LocationCity_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.LocationCity_Id)
                .ForeignKey("dbo.AD_LocationCities", t => t.LocationCity_ParentId)
                .Index(t => t.LocationCity_ParentId);
            
            CreateTable(
                "dbo.AD_UserMetas",
                c => new
                    {
                        UserMeta_Id = c.Guid(nullable: false),
                        UserMeta_FirstName = c.String(),
                        UserMeta_LastName = c.String(),
                        UserMeta_DisplayName = c.String(),
                        UserMeta_NationalCode = c.String(),
                        UserMeta_BirthOn = c.DateTime(),
                        UserMeta_MarriedOn = c.DateTime(),
                        UserMeta_AvatarFileName = c.String(),
                        UserMeta_IsActive = c.Boolean(),
                        UserMeta_Gender = c.Int(),
                        UserMeta_AboutMe = c.String(),
                        UserMeta_HomeNumber = c.String(),
                        UserMeta_PhoneNumber = c.String(),
                        UserMeta_LocationId = c.Guid(),
                        UserMeta_CreatedById = c.Guid(),
                        UserMeta_CreatedOn = c.DateTime(),
                        UserMeta_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserMeta_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserMeta_CreatedById)
                .ForeignKey("dbo.AD_Locations", t => t.UserMeta_LocationId)
                .Index(t => t.UserMeta_LocationId)
                .Index(t => t.UserMeta_CreatedById);
            
            CreateTable(
                "dbo.AD_Receipts",
                c => new
                    {
                        Receipt_Id = c.Guid(nullable: false),
                        Receipt_IsBuy = c.Boolean(),
                        Receipt_Payment = c.Int(),
                        Receipt_FirstName = c.String(),
                        Receipt_LastName = c.String(),
                        Receipt_NationalCode = c.String(),
                        Receipt_TransfereeName = c.String(),
                        Receipt_PhoneNumber = c.String(),
                        Receipt_HomeNumber = c.String(),
                        Receipt_Email = c.String(),
                        Receipt_TotalProductsPrice = c.Decimal(precision: 18, scale: 2),
                        Receipt_TransportationCost = c.Decimal(precision: 18, scale: 2),
                        Receipt_FinalPrice = c.Decimal(precision: 18, scale: 2),
                        Receipt_ConfirmedOn = c.DateTime(),
                        Receipt_TrackingCode = c.String(),
                        Receipt_InvoiceNumber = c.String(),
                        Receipt_LocationId = c.Guid(),
                        Receipt_CreatedById = c.Guid(),
                        Receipt_CreatedOn = c.DateTime(),
                        Receipt_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Receipt_Id)
                .ForeignKey("dbo.AD_Users", t => t.Receipt_CreatedById)
                .ForeignKey("dbo.AD_Locations", t => t.Receipt_LocationId)
                .Index(t => t.Receipt_LocationId)
                .Index(t => t.Receipt_CreatedById);
            
            CreateTable(
                "dbo.AD_ReceiptOptions",
                c => new
                    {
                        ReceiptOption_Id = c.Guid(nullable: false),
                        ReceiptOption_Title = c.String(),
                        ReceiptOption_Code = c.String(),
                        ReceiptOption_Price = c.Decimal(precision: 18, scale: 2),
                        ReceiptOption_Count = c.Decimal(precision: 18, scale: 2),
                        ReceiptOption_TotalPrice = c.Decimal(precision: 18, scale: 2),
                        ReceiptOption_PreviousPrice = c.Decimal(precision: 18, scale: 2),
                        ReceiptOption_DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        ReceiptOption_CompanyTitle = c.String(),
                        ReceiptOption_CompanyCode = c.String(),
                        ReceiptOption_CategoryTitle = c.String(),
                        ReceiptOption_CategoryCode = c.String(),
                        ReceiptOption_SaledById = c.Guid(),
                        ReceiptOption_ReceiptId = c.Guid(),
                        ReceiptOption_CreatedById = c.Guid(),
                        ReceiptOption_CreatedOn = c.DateTime(),
                        ReceiptOption_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ReceiptOption_Id)
                .ForeignKey("dbo.AD_Users", t => t.ReceiptOption_CreatedById)
                .ForeignKey("dbo.AD_Receipts", t => t.ReceiptOption_ReceiptId)
                .Index(t => t.ReceiptOption_ReceiptId)
                .Index(t => t.ReceiptOption_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyQuestions",
                c => new
                    {
                        CompanyQuestion_Id = c.Guid(nullable: false),
                        CompanyQuestion_Title = c.String(),
                        CompanyQuestion_Body = c.String(),
                        CompanyQuestion_State = c.Int(),
                        CompanyQuestion_RejectDescription = c.String(),
                        CompanyQuestion_ReplyId = c.Guid(),
                        CompanyQuestion_CompanyId = c.Guid(),
                        CompanyQuestion_QuestionedById = c.Guid(),
                        CompanyQuestion_ApprovedById = c.Guid(),
                        CompanyQuestion_CreatedById = c.Guid(),
                        CompanyQuestion_ModifiedById = c.Guid(),
                        CompanyQuestion_CreatedOn = c.DateTime(),
                        CompanyQuestion_IsDelete = c.Boolean(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.CompanyQuestion_Id)
                .ForeignKey("dbo.AD_Users", t => t.CompanyQuestion_ApprovedById)
                .ForeignKey("dbo.AD_CompanyQuestions", t => t.CompanyQuestion_ReplyId)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyQuestion_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyQuestion_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.CompanyQuestion_ModifiedById)
                .ForeignKey("dbo.AD_Users", t => t.CompanyQuestion_QuestionedById)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.CompanyQuestion_ReplyId)
                .Index(t => t.CompanyQuestion_CompanyId)
                .Index(t => t.CompanyQuestion_QuestionedById)
                .Index(t => t.CompanyQuestion_ApprovedById)
                .Index(t => t.CompanyQuestion_CreatedById)
                .Index(t => t.CompanyQuestion_ModifiedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_CompanyQuestionLikes",
                c => new
                    {
                        CompanyQuestionLike_Id = c.Guid(nullable: false),
                        CompanyQuestionLike_IsLike = c.Boolean(),
                        CompanyQuestionLike_LikedById = c.Guid(),
                        CompanyQuestionLike_QuestionId = c.Guid(),
                        CompanyQuestionLike_CreatedOn = c.DateTime(),
                        CompanyQuestionLike_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyQuestionLike_Id)
                .ForeignKey("dbo.AD_Users", t => t.CompanyQuestionLike_LikedById)
                .ForeignKey("dbo.AD_CompanyQuestions", t => t.CompanyQuestionLike_QuestionId)
                .Index(t => t.CompanyQuestionLike_LikedById)
                .Index(t => t.CompanyQuestionLike_QuestionId);
            
            CreateTable(
                "dbo.AD_CompanyReviews",
                c => new
                    {
                        CompanyReview_Id = c.Guid(nullable: false),
                        CompanyReview_State = c.Int(),
                        CompanyReview_RejectDescription = c.String(),
                        CompanyReview_CompanyId = c.Guid(),
                        CompanyReview_ApprovedById = c.Guid(),
                        CompanyReview_Body = c.String(),
                        CompanyReview_Title = c.String(),
                        CompanyReview_IsActive = c.Boolean(),
                        CompanyReview_CreatedOn = c.DateTime(),
                        CompanyReview_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyReview_Id)
                .ForeignKey("dbo.AD_Users", t => t.CompanyReview_ApprovedById)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyReview_CompanyId)
                .Index(t => t.CompanyReview_CompanyId)
                .Index(t => t.CompanyReview_ApprovedById);
            
            CreateTable(
                "dbo.AD_CompanySlides",
                c => new
                    {
                        CompanySlide_Id = c.Guid(nullable: false),
                        CompanySlide_Title = c.String(),
                        CompanySlide_ImageFileName = c.String(),
                        CompanySlide_Description = c.String(),
                        CompanySlide_Order = c.Int(nullable: false),
                        CompanySlide_CompanyId = c.Guid(),
                        CompanySlide_ProductId = c.Guid(),
                        CompanySlide_CreatedOn = c.DateTime(),
                        CompanySlide_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanySlide_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanySlide_CompanyId)
                .ForeignKey("dbo.AD_Products", t => t.CompanySlide_ProductId)
                .Index(t => t.CompanySlide_CompanyId)
                .Index(t => t.CompanySlide_ProductId);
            
            CreateTable(
                "dbo.AD_CompanySocials",
                c => new
                    {
                        CompanySocial_Id = c.Guid(nullable: false),
                        CompanySocial_TwitterLink = c.String(),
                        CompanySocial_FacebookLink = c.String(),
                        CompanySocial_GooglePlusLink = c.String(),
                        CompanySocial_YoutubeLink = c.String(),
                        CompanySocial_InstagramLink = c.String(),
                        CompanySocial_TelegramLink = c.String(),
                        CompanySocial_CompanyId = c.Guid(),
                        CompanySocial_CreatedById = c.Guid(),
                        CompanySocial_CreatedOn = c.DateTime(),
                        CompanySocial_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanySocial_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanySocial_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanySocial_CreatedById)
                .Index(t => t.CompanySocial_CompanyId)
                .Index(t => t.CompanySocial_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyVisits",
                c => new
                    {
                        CompanyVisit_Id = c.Guid(nullable: false),
                        CompanyVisit_VisitedById = c.Guid(),
                        CompanyVisit_CompanyId = c.Guid(),
                        CompanyVisit_CreatedById = c.Guid(),
                        CompanyVisit_IsVisit = c.Boolean(),
                        CompanyVisit_CreatedOn = c.DateTime(),
                        CompanyVisit_IsDelete = c.Boolean(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.CompanyVisit_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyVisit_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyVisit_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.CompanyVisit_VisitedById)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.CompanyVisit_VisitedById)
                .Index(t => t.CompanyVisit_CompanyId)
                .Index(t => t.CompanyVisit_CreatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_ProductFeatures",
                c => new
                    {
                        ProductFeature_Id = c.Guid(nullable: false),
                        ProductFeature_Title = c.String(),
                        ProductFeature_ProductId = c.Guid(),
                        ProductFeature_CreatedOn = c.DateTime(),
                        ProductFeature_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductFeature_Id)
                .ForeignKey("dbo.AD_Products", t => t.ProductFeature_ProductId)
                .Index(t => t.ProductFeature_ProductId);
            
            CreateTable(
                "dbo.AD_Guarantees",
                c => new
                    {
                        Guarantee_Id = c.Guid(nullable: false),
                        Guarantee_Description = c.String(),
                        Guarantee_Email = c.String(),
                        Guarantee_MobileNumber = c.String(),
                        Guarantee_PhoneNumber = c.String(),
                        Guarantee_Title = c.String(),
                        Guarantee_CreatedOn = c.DateTime(),
                        Guarantee_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Guarantee_Id);
            
            CreateTable(
                "dbo.AD_ProductImages",
                c => new
                    {
                        ProductImage_Id = c.Guid(nullable: false),
                        ProductImage_Order = c.Int(),
                        ProductImage_IsWatermark = c.Boolean(),
                        ProductImage_ProductId = c.Guid(),
                        ProductImage_CreatedById = c.Guid(),
                        ProductImage_Title = c.String(),
                        ProductImage_FileName = c.String(),
                        ProductImage_FileSize = c.String(),
                        ProductImage_FileExtension = c.String(),
                        ProductImage_FileDimension = c.String(),
                        ProductImage_CreatedOn = c.DateTime(),
                        ProductImage_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductImage_Id)
                .ForeignKey("dbo.AD_Users", t => t.ProductImage_CreatedById)
                .ForeignKey("dbo.AD_Products", t => t.ProductImage_ProductId)
                .Index(t => t.ProductImage_ProductId)
                .Index(t => t.ProductImage_CreatedById);
            
            CreateTable(
                "dbo.AD_ProductLikes",
                c => new
                    {
                        ProductLike_Id = c.Guid(nullable: false),
                        ProductLike_IsLike = c.Boolean(),
                        ProductLike_LikedById = c.Guid(),
                        ProductLike_ProductId = c.Guid(),
                        ProductLike_CreatedOn = c.DateTime(),
                        ProductLike_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductLike_Id)
                .ForeignKey("dbo.AD_Users", t => t.ProductLike_LikedById)
                .ForeignKey("dbo.AD_Products", t => t.ProductLike_ProductId)
                .Index(t => t.ProductLike_LikedById)
                .Index(t => t.ProductLike_ProductId);
            
            CreateTable(
                "dbo.AD_ProductReviews",
                c => new
                    {
                        ProductReview_Id = c.Guid(nullable: false),
                        ProductReview_State = c.Int(),
                        ProductReview_RejectDescription = c.String(),
                        ProductReview_ProductId = c.Guid(),
                        ProductReview_ApprovedById = c.Guid(),
                        ProductReview_CreatedById = c.Guid(),
                        ProductReview_Body = c.String(),
                        ProductReview_Title = c.String(),
                        ProductReview_IsActive = c.Boolean(),
                        ProductReview_CreatedOn = c.DateTime(),
                        ProductReview_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductReview_Id)
                .ForeignKey("dbo.AD_Users", t => t.ProductReview_ApprovedById)
                .ForeignKey("dbo.AD_Users", t => t.ProductReview_CreatedById)
                .ForeignKey("dbo.AD_Products", t => t.ProductReview_ProductId)
                .Index(t => t.ProductReview_ProductId)
                .Index(t => t.ProductReview_ApprovedById)
                .Index(t => t.ProductReview_CreatedById);
            
            CreateTable(
                "dbo.AD_ProductTags",
                c => new
                    {
                        ProductTag_Id = c.Guid(nullable: false),
                        ProductTag_StartedOn = c.DateTime(),
                        ProductTag_ExpiredOn = c.DateTime(),
                        ProductTag_TagId = c.Guid(),
                        ProductTag_ProductId = c.Guid(),
                        ProductTag_CreatedOn = c.DateTime(),
                        ProductTag_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductTag_Id)
                .ForeignKey("dbo.AD_Products", t => t.ProductTag_ProductId)
                .ForeignKey("dbo.AD_Tags", t => t.ProductTag_TagId)
                .Index(t => t.ProductTag_TagId)
                .Index(t => t.ProductTag_ProductId);
            
            CreateTable(
                "dbo.AD_Tags",
                c => new
                    {
                        Tag_Id = c.Guid(nullable: false),
                        Tag_Code = c.String(),
                        Tag_Title = c.String(),
                        Tag_Description = c.String(),
                        Tag_LogoFileName = c.String(),
                        Tag_CostValue = c.String(),
                        Tag_DurationDay = c.String(),
                        Tag_Color = c.Int(nullable: false),
                        Tag_IsActive = c.Boolean(),
                        Tag_CreatedOn = c.DateTime(),
                        Tag_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Tag_Id);
            
            CreateTable(
                "dbo.AD_ProductVisits",
                c => new
                    {
                        ProductVisit_Id = c.Guid(nullable: false),
                        ProductVisit_IpAddress = c.Boolean(),
                        ProductVisit_VisitedById = c.Guid(),
                        ProductVisit_ProductId = c.Guid(),
                        ProductVisit_IsVisit = c.Boolean(),
                        ProductVisit_CreatedOn = c.DateTime(),
                        ProductVisit_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductVisit_Id)
                .ForeignKey("dbo.AD_Products", t => t.ProductVisit_ProductId)
                .ForeignKey("dbo.AD_Users", t => t.ProductVisit_VisitedById)
                .Index(t => t.ProductVisit_VisitedById)
                .Index(t => t.ProductVisit_ProductId);
            
            CreateTable(
                "dbo.AD_CategoryFollows",
                c => new
                    {
                        CategoryFollow_Id = c.Guid(nullable: false),
                        CategoryFollow_FollowedById = c.Guid(),
                        CategoryFollow_CategoryId = c.Guid(),
                        CategoryFollow_IsFollow = c.Boolean(),
                        CategoryFollow_CreatedOn = c.DateTime(),
                        CategoryFollow_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryFollow_Id)
                .ForeignKey("dbo.AD_Categories", t => t.CategoryFollow_CategoryId)
                .ForeignKey("dbo.AD_Users", t => t.CategoryFollow_FollowedById)
                .Index(t => t.CategoryFollow_FollowedById)
                .Index(t => t.CategoryFollow_CategoryId);
            
            CreateTable(
                "dbo.AD_CategoryReviews",
                c => new
                    {
                        CategoryReview_Id = c.Guid(nullable: false),
                        CategoryReview_CategoryId = c.Guid(),
                        CategoryReview_CreatedById = c.Guid(),
                        CategoryReview_Body = c.String(),
                        CategoryReview_Title = c.String(),
                        CategoryReview_IsActive = c.Boolean(),
                        CategoryReview_CreatedOn = c.DateTime(),
                        CategoryReview_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryReview_Id)
                .ForeignKey("dbo.AD_Categories", t => t.CategoryReview_CategoryId)
                .ForeignKey("dbo.AD_Users", t => t.CategoryReview_CreatedById)
                .Index(t => t.CategoryReview_CategoryId)
                .Index(t => t.CategoryReview_CreatedById);
            
            CreateTable(
                "dbo.AD_AnnouncePayments",
                c => new
                    {
                        AnnouncePayment_Id = c.Guid(nullable: false),
                        AnnouncePayment_ReferenceCode = c.Long(),
                        AnnouncePayment_Amount = c.Decimal(precision: 18, scale: 2),
                        AnnouncePayment_Type = c.Int(),
                        AnnouncePayment_AuthorityCode = c.String(),
                        AnnouncePayment_Description = c.String(),
                        AnnouncePayment_IsComplete = c.Boolean(),
                        AnnouncePayment_IsSuccess = c.Boolean(),
                        AnnouncePayment_MerchantCode = c.String(),
                        AnnouncePayment_StatusCode = c.Int(),
                        AnnouncePayment_AnnounceId = c.Guid(),
                        AnnouncePayment_CreatedById = c.Guid(),
                        AnnouncePayment_CreatedOn = c.DateTime(),
                        AnnouncePayment_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.AnnouncePayment_Id)
                .ForeignKey("dbo.AD_Announces", t => t.AnnouncePayment_AnnounceId)
                .ForeignKey("dbo.AD_Users", t => t.AnnouncePayment_CreatedById)
                .Index(t => t.AnnouncePayment_AnnounceId)
                .Index(t => t.AnnouncePayment_CreatedById);
            
            CreateTable(
                "dbo.AD_AnnounceReserves",
                c => new
                    {
                        AnnounceReserve_Id = c.Guid(nullable: false),
                        AnnounceReserve_Day = c.DateTime(),
                        AnnounceReserve_AnnounceId = c.Guid(),
                        AnnounceReserve_CreatedOn = c.DateTime(),
                        AnnounceReserve_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.AnnounceReserve_Id)
                .ForeignKey("dbo.AD_Announces", t => t.AnnounceReserve_AnnounceId)
                .Index(t => t.AnnounceReserve_AnnounceId);
            
            CreateTable(
                "dbo.AD_UserBudgets",
                c => new
                    {
                        UserBudget_Id = c.Guid(nullable: false),
                        UserBudget_RemainValue = c.Int(),
                        UserBudget_IncDecValue = c.Int(),
                        UserBudget_Description = c.String(),
                        UserBudget_PaymentId = c.Guid(),
                        UserBudget_CreatedOn = c.DateTime(),
                        UserBudget_IsDelete = c.Boolean(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.UserBudget_Id)
                .ForeignKey("dbo.AD_ReceiptPayments", t => t.UserBudget_PaymentId)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.UserBudget_PaymentId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_ReceiptPayments",
                c => new
                    {
                        ReceiptPayment_Id = c.Guid(nullable: false),
                        ReceiptPayment_MerchantCode = c.String(),
                        ReceiptPayment_AuthorityCode = c.String(),
                        ReceiptPayment_ReferenceCode = c.Long(),
                        ReceiptPayment_StatusCode = c.Int(),
                        ReceiptPayment_Pay = c.Int(nullable: false),
                        ReceiptPayment_Buy = c.Int(nullable: false),
                        ReceiptPayment_IsComplete = c.Boolean(),
                        ReceiptPayment_IsSuccess = c.Boolean(),
                        ReceiptPayment_Amount = c.Int(),
                        ReceiptPayment_Description = c.String(),
                        ReceiptPayment_MobileNumber = c.String(),
                        ReceiptPayment_Email = c.String(),
                        ReceiptPayment_PayedById = c.Guid(),
                        ReceiptPayment_ReceiptId = c.Guid(),
                        ReceiptPayment_CreatedById = c.Guid(),
                        ReceiptPayment_CreatedOn = c.DateTime(),
                        ReceiptPayment_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ReceiptPayment_Id)
                .ForeignKey("dbo.AD_Users", t => t.ReceiptPayment_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.ReceiptPayment_PayedById)
                .ForeignKey("dbo.AD_Receipts", t => t.ReceiptPayment_ReceiptId)
                .Index(t => t.ReceiptPayment_PayedById)
                .Index(t => t.ReceiptPayment_ReceiptId)
                .Index(t => t.ReceiptPayment_CreatedById);
            
            CreateTable(
                "dbo.AD_UserClaims",
                c => new
                    {
                        UserClaim_Id = c.Int(nullable: false, identity: true),
                        UserClaim_UserId = c.Guid(nullable: false),
                        UserClaim_ClaimType = c.String(),
                        UserClaim_ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaim_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserClaim_UserId, cascadeDelete: true)
                .Index(t => t.UserClaim_UserId);
            
            CreateTable(
                "dbo.AD_UserLogins",
                c => new
                    {
                        UserLogin_LoginProvider = c.String(nullable: false, maxLength: 128),
                        UserLogin_ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserLogin_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserLogin_LoginProvider, t.UserLogin_ProviderKey, t.UserLogin_UserId })
                .ForeignKey("dbo.AD_Users", t => t.UserLogin_UserId, cascadeDelete: true)
                .Index(t => t.UserLogin_UserId);
            
            CreateTable(
                "dbo.AD_ProductCommentLikes",
                c => new
                    {
                        ProductCommentLike_Id = c.Guid(nullable: false),
                        ProductCommentLike_IsLike = c.Boolean(),
                        ProductCommentLike_LikedById = c.Guid(),
                        ProductCommentLike_CommentId = c.Guid(),
                        ProductCommentLike_CreatedOn = c.DateTime(),
                        ProductCommentLike_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductCommentLike_Id)
                .ForeignKey("dbo.AD_ProductComments", t => t.ProductCommentLike_CommentId)
                .ForeignKey("dbo.AD_Users", t => t.ProductCommentLike_LikedById)
                .Index(t => t.ProductCommentLike_LikedById)
                .Index(t => t.ProductCommentLike_CommentId);
            
            CreateTable(
                "dbo.AD_UserViolations",
                c => new
                    {
                        UserViolation_Id = c.Guid(nullable: false),
                        UserViolation_Type = c.Int(),
                        UserViolation_Reason = c.Int(),
                        UserViolation_ReasonDescription = c.String(),
                        UserViolation_IsRead = c.Boolean(),
                        UserViolation_TargetId = c.Guid(),
                        UserViolation_ReportedById = c.Guid(),
                        UserViolation_CreatedOn = c.DateTime(),
                        UserViolation_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserViolation_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserViolation_ReportedById)
                .Index(t => t.UserViolation_ReportedById);
            
            CreateTable(
                "dbo.AD_UserRoles",
                c => new
                    {
                        UserRole_UserId = c.Guid(nullable: false),
                        UserRole_RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_UserId, t.UserRole_RoleId })
                .ForeignKey("dbo.AD_Users", t => t.UserRole_UserId, cascadeDelete: true)
                .ForeignKey("dbo.AD_Roles", t => t.UserRole_RoleId, cascadeDelete: true)
                .Index(t => t.UserRole_UserId)
                .Index(t => t.UserRole_RoleId);
            
            CreateTable(
                "dbo.AD_UserSettings",
                c => new
                    {
                        UserSetting_Id = c.Guid(nullable: false),
                        UserSetting_Language = c.Int(),
                        UserSetting_Theme = c.Int(),
                        UserSetting_IsEnableSpecificationletter = c.Boolean(),
                        UserSetting_IsHideSpecificationletterBlock = c.Boolean(),
                        UserSetting_IsEnableDateOfBirth = c.Boolean(),
                        UserSetting_CreatedById = c.Guid(),
                        UserSetting_CreatedOn = c.DateTime(),
                        UserSetting_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserSetting_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserSetting_CreatedById)
                .Index(t => t.UserSetting_CreatedById);
            
            CreateTable(
                "dbo.AD_RolePermissions",
                c => new
                    {
                        RolePermission_Id = c.Guid(nullable: false),
                        RolePermission_RoleId = c.Guid(nullable: false),
                        RolePermission_PermissionId = c.Guid(nullable: false),
                        RolePermission_CreatedOn = c.DateTime(),
                        RolePermission_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.RolePermission_Id)
                .ForeignKey("dbo.AD_Permissions", t => t.RolePermission_PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.AD_Roles", t => t.RolePermission_RoleId, cascadeDelete: true)
                .Index(t => t.RolePermission_RoleId)
                .Index(t => t.RolePermission_PermissionId);
            
            CreateTable(
                "dbo.AD_Permissions",
                c => new
                    {
                        Permission_Id = c.Guid(nullable: false),
                        Permission_Title = c.String(),
                        Permission_Name = c.String(),
                        Permission_MethodName = c.String(),
                        Permission_Description = c.String(),
                        Permission_Order = c.Int(),
                        Permission_IsPermission = c.Boolean(),
                        Permission_IsCallback = c.Boolean(),
                        Permission_ParentId = c.Guid(),
                        Permission_CreatedOn = c.DateTime(),
                        Permission_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Permission_Id)
                .ForeignKey("dbo.AD_Permissions", t => t.Permission_ParentId)
                .Index(t => t.Permission_ParentId);
            
            CreateTable(
                "dbo.AD_UserNotifications",
                c => new
                    {
                        UserNotification_Id = c.Guid(nullable: false),
                        UserNotification_IsRead = c.Boolean(),
                        UserNotification_Url = c.String(),
                        UserNotification_Message = c.String(),
                        UserNotification_ReadOn = c.DateTime(),
                        UserNotification_Type = c.Int(nullable: false),
                        UserNotification_TargetId = c.Guid(),
                        UserNotification_OwnedById = c.Guid(),
                        UserNotification_TargetTitle = c.String(),
                        UserNotification_TargetImage = c.String(),
                        UserNotification_CreatedById = c.Guid(),
                        UserNotification_CreatedOn = c.DateTime(),
                        UserNotification_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserNotification_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserNotification_CreatedById)
                .ForeignKey("dbo.AD_Products", t => t.UserNotification_TargetId)
                .Index(t => t.UserNotification_TargetId)
                .Index(t => t.UserNotification_CreatedById);
            
            CreateTable(
                "dbo.AD_UserOnlines",
                c => new
                    {
                        UserOnline_Id = c.Guid(nullable: false),
                        UserOnline_SessionId = c.String(),
                        UserOnline_IsActive = c.Boolean(nullable: false),
                        UserOnline_CreatedById = c.Guid(),
                        UserOnline_CreatedOn = c.DateTime(),
                        UserOnline_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserOnline_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserOnline_CreatedById)
                .Index(t => t.UserOnline_CreatedById);
            
            CreateTable(
                "dbo.AD_UserOperators",
                c => new
                    {
                        UserOperator_Id = c.Guid(nullable: false),
                        UserOperator_PaymentType = c.Int(),
                        UserOperator_Amount = c.Decimal(precision: 18, scale: 2),
                        UserOperator_Description = c.String(),
                        UserOperator_CreatedById = c.Guid(),
                        UserOperator_CreatedOn = c.DateTime(),
                        UserOperator_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserOperator_Id)
                .ForeignKey("dbo.AD_Users", t => t.UserOperator_CreatedById)
                .Index(t => t.UserOperator_CreatedById);
            
            CreateTable(
                "dbo.AD_Statistics",
                c => new
                    {
                        Statistic_Id = c.Guid(nullable: false),
                        Statistic_ActionName = c.String(),
                        Statistic_ControllerName = c.String(),
                        Statistic_IpAddress = c.String(),
                        Statistic_Latitude = c.String(),
                        Statistic_Longitude = c.String(),
                        Statistic_Referrer = c.String(),
                        Statistic_UserAgent = c.String(),
                        Statistic_UserOs = c.String(),
                        Statistic_ViewedOn = c.DateTime(),
                        Statistic_CreatedOn = c.DateTime(),
                        Statistic_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Statistic_Id);
            
            CreateTable(
                "dbo.AD_Sms",
                c => new
                    {
                        Sms_Id = c.Guid(nullable: false),
                        Sms_Title = c.String(),
                        Sms_Body = c.String(),
                        Sms_IsSend = c.Boolean(),
                        Sms_SentById = c.Guid(),
                        Sms_RecievedById = c.Guid(),
                        Sms_CreatedOn = c.DateTime(),
                        Sms_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Sms_Id)
                .ForeignKey("dbo.AD_Users", t => t.Sms_RecievedById)
                .ForeignKey("dbo.AD_Users", t => t.Sms_SentById)
                .Index(t => t.Sms_SentById)
                .Index(t => t.Sms_RecievedById);
            
            CreateTable(
                "dbo.AD_SmsOperators",
                c => new
                    {
                        SmsOperator_Id = c.Guid(nullable: false),
                        SmsOperator_IsActive = c.Boolean(nullable: false),
                        SmsOperator_IsAllowExecuteCommand = c.Boolean(nullable: false),
                        SmsOperator_IsAllowReadCommand = c.Boolean(nullable: false),
                        SmsOperator_MobileNumber = c.String(),
                        SmsOperator_CreatedOn = c.DateTime(),
                        SmsOperator_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.SmsOperator_Id);
            
            CreateTable(
                "dbo.AD_Settings",
                c => new
                    {
                        Setting_Id = c.Guid(nullable: false),
                        Setting_IsAllowViewingProfiles = c.Boolean(nullable: false),
                        Setting_IsDefaultAvatarEnabled = c.Boolean(nullable: false),
                        Setting_FacebookPageUrl = c.String(),
                        Setting_GooglePlusPageUrl = c.String(),
                        Setting_InstagramPageUrl = c.String(),
                        Setting_LinkedinPageUrl = c.String(),
                        Setting_SiteDescription = c.String(),
                        Setting_SiteEmail = c.String(),
                        Setting_SiteShortTitle = c.String(),
                        Setting_SiteTitle = c.String(),
                        Setting_SiteVersion = c.String(),
                        Setting_TelegramPageUrl = c.String(),
                        Setting_VideoMaximumSizeBytes = c.String(),
                        Setting_CreatedOn = c.DateTime(),
                        Setting_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Setting_Id);
            
            CreateTable(
                "dbo.AD_SettingTransactions",
                c => new
                    {
                        SettingTransaction_Id = c.Guid(nullable: false),
                        SettingTransaction_NameOfAccountNumber = c.String(),
                        SettingTransaction_CorporationShebaNumber = c.String(),
                        SettingTransaction_CorporationShetabNumber = c.String(),
                        SettingTransaction_CorporationAccountNumber = c.String(),
                        SettingTransaction_BankName = c.String(),
                        SettingTransaction_CreatedOn = c.DateTime(),
                        SettingTransaction_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.SettingTransaction_Id);
            
            CreateTable(
                "dbo.AD_Seos",
                c => new
                    {
                        Seo_Id = c.Guid(nullable: false),
                        Seo_EntityId = c.String(),
                        Seo_EntityName = c.String(),
                        Seo_IsActive = c.Boolean(nullable: false),
                        Seo_Language = c.Int(),
                        Seo_Slug = c.String(),
                        Seo_CreatedOn = c.DateTime(),
                        Seo_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Seo_Id);
            
            CreateTable(
                "dbo.AD_SeoSettings",
                c => new
                    {
                        SeoSetting_Id = c.Guid(nullable: false),
                        SeoSetting_IsAllowUnicodeChars = c.Boolean(nullable: false),
                        SeoSetting_IsCanonicalUrlEnabled = c.Boolean(nullable: false),
                        SeoSetting_HasConvertNonWesternChars = c.Boolean(nullable: false),
                        SeoSetting_CustomHeadTags = c.String(),
                        SeoSetting_DefaultMetaDescription = c.String(),
                        SeoSetting_DefaultMetaKeywords = c.String(),
                        SeoSetting_DefaultTitle = c.String(),
                        SeoSetting_IsCssBundlingEnabled = c.Boolean(nullable: false),
                        SeoSetting_IsJsBundlingEnabled = c.Boolean(nullable: false),
                        SeoSetting_GenerateMetaDescription = c.String(),
                        SeoSetting_HasOpenGraphMetaTags = c.Boolean(nullable: false),
                        SeoSetting_PageTitleSeparator = c.String(),
                        SeoSetting_HasTwitterMetaTags = c.Boolean(nullable: false),
                        SeoSetting_WwwRequirement = c.Int(nullable: false),
                        SeoSetting_CreatedOn = c.DateTime(),
                        SeoSetting_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.SeoSetting_Id);
            
            CreateTable(
                "dbo.AD_SeoUrls",
                c => new
                    {
                        SeoUrl_Id = c.Guid(nullable: false),
                        SeoUrl_AbsoulateUrl = c.String(),
                        SeoUrl_CurrentUrl = c.String(),
                        SeoUrl_IsActive = c.Boolean(),
                        SeoUrl_Redirection = c.Int(),
                        SeoUrl_CreatedById = c.Guid(),
                        SeoUrl_CreatedOn = c.DateTime(),
                        SeoUrl_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.SeoUrl_Id)
                .ForeignKey("dbo.AD_Users", t => t.SeoUrl_CreatedById)
                .Index(t => t.SeoUrl_CreatedById);
            
            CreateTable(
                "dbo.AD_Reports",
                c => new
                    {
                        Report_Id = c.Guid(nullable: false),
                        Report_Content = c.String(storeType: "xml"),
                        Report_Description = c.String(),
                        Report_IsActive = c.Boolean(nullable: false),
                        Report_Name = c.String(),
                        Report_Order = c.Int(nullable: false),
                        Report_Title = c.String(),
                        Report_HasChild = c.Boolean(),
                        Report_Level = c.Int(),
                        Report_CreatedById = c.Guid(),
                        Report_ParentId = c.Guid(),
                        Report_CreatedOn = c.DateTime(),
                        Report_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Report_Id)
                .ForeignKey("dbo.AD_Users", t => t.Report_CreatedById)
                .ForeignKey("dbo.AD_Reports", t => t.Report_ParentId)
                .Index(t => t.Report_CreatedById)
                .Index(t => t.Report_ParentId);
            
            CreateTable(
                "dbo.AD_ProductNotifies",
                c => new
                    {
                        ProductNotify_Id = c.Guid(nullable: false),
                        ProductNotify_ProductId = c.Guid(),
                        ProductNotify_CreatedById = c.Guid(),
                        ProductNotify_CreatedOn = c.DateTime(),
                        ProductNotify_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductNotify_Id)
                .ForeignKey("dbo.AD_Users", t => t.ProductNotify_CreatedById)
                .ForeignKey("dbo.AD_Products", t => t.ProductNotify_ProductId)
                .Index(t => t.ProductNotify_ProductId)
                .Index(t => t.ProductNotify_CreatedById);
            
            CreateTable(
                "dbo.AD_ProductRates",
                c => new
                    {
                        ProductRate_Id = c.Guid(nullable: false),
                        ProductRate_ProductId = c.Guid(),
                        ProductRate_CreatedById = c.Guid(),
                        ProductRate_IsApprove = c.Boolean(),
                        ProductRate_Rate = c.Int(),
                        ProductRate_CreatedOn = c.DateTime(),
                        ProductRate_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductRate_Id)
                .ForeignKey("dbo.AD_Users", t => t.ProductRate_CreatedById)
                .ForeignKey("dbo.AD_Products", t => t.ProductRate_ProductId)
                .Index(t => t.ProductRate_ProductId)
                .Index(t => t.ProductRate_CreatedById);
            
            CreateTable(
                "dbo.AD_ProductVideos",
                c => new
                    {
                        ProductVideo_Id = c.Guid(nullable: false),
                        ProductVideo_CreatedOn = c.DateTime(),
                        ProductVideo_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductVideo_Id);
            
            CreateTable(
                "dbo.AD_Plans",
                c => new
                    {
                        Plan_Id = c.Guid(nullable: false),
                        Plan_Code = c.String(),
                        Plan_DurationDay = c.Int(),
                        Plan_Price = c.Decimal(precision: 18, scale: 2),
                        Plan_RoleId = c.Guid(),
                        Plan_Title = c.String(),
                        Plan_PreviousePrice = c.Decimal(precision: 18, scale: 2),
                        Plan_Color = c.Int(),
                        Plan_IsActive = c.Boolean(),
                        Plan_PlanDuration = c.Int(),
                        Plan_ModifiedOn = c.DateTime(),
                        Plan_CreatedById = c.Guid(),
                        Plan_CreatedOn = c.DateTime(),
                        Plan_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Plan_Id)
                .ForeignKey("dbo.AD_Users", t => t.Plan_CreatedById)
                .ForeignKey("dbo.AD_Roles", t => t.Plan_RoleId)
                .Index(t => t.Plan_RoleId)
                .Index(t => t.Plan_CreatedById);
            
            CreateTable(
                "dbo.AD_PlanDiscounts",
                c => new
                    {
                        PlanDiscount_Id = c.Guid(nullable: false),
                        PlanDiscount_Code = c.String(),
                        PlanDiscount_Percent = c.Int(),
                        PlanDiscount_Max = c.Int(),
                        PlanDiscount_Count = c.Int(),
                        PlanDiscount_Expire = c.DateTime(),
                        PlanDiscount_CreatedById = c.Guid(),
                        PlanDiscount_CreatedOn = c.DateTime(),
                        PlanDiscount_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.PlanDiscount_Id)
                .ForeignKey("dbo.AD_Users", t => t.PlanDiscount_CreatedById)
                .Index(t => t.PlanDiscount_CreatedById);
            
            CreateTable(
                "dbo.AD_PlanPayments",
                c => new
                    {
                        PlanPayment_Id = c.Guid(nullable: false),
                        PlanPayment_Amount = c.Decimal(precision: 18, scale: 2),
                        PlanPayment_AuthorityCode = c.String(),
                        PlanPayment_Description = c.String(),
                        PlanPayment_IsComplete = c.Boolean(),
                        PlanPayment_IsSuccess = c.Boolean(),
                        PlanPayment_MerchantCode = c.String(),
                        PlanPayment_ReferenceCode = c.Long(),
                        PlanPayment_StatusCode = c.Int(),
                        PlanPayment_PlanId = c.Guid(),
                        PlanPayment_PlanDiscountId = c.Guid(),
                        PlanPayment_CreatedById = c.Guid(),
                        PlanPayment_CreatedOn = c.DateTime(),
                        PlanPayment_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.PlanPayment_Id)
                .ForeignKey("dbo.AD_Users", t => t.PlanPayment_CreatedById)
                .ForeignKey("dbo.AD_Plans", t => t.PlanPayment_PlanId)
                .ForeignKey("dbo.AD_PlanDiscounts", t => t.PlanPayment_PlanDiscountId)
                .Index(t => t.PlanPayment_PlanId)
                .Index(t => t.PlanPayment_PlanDiscountId)
                .Index(t => t.PlanPayment_CreatedById);
            
            CreateTable(
                "dbo.AD_PlanOptions",
                c => new
                    {
                        PlanOption_Id = c.Guid(nullable: false),
                        PlanOption_CreatedOn = c.DateTime(),
                        PlanOption_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.PlanOption_Id);
            
            CreateTable(
                "dbo.AD_Notices",
                c => new
                    {
                        Notice_Id = c.Guid(nullable: false),
                        Notice_Title = c.String(),
                        Notice_Body = c.String(),
                        Notice_IsActive = c.Boolean(),
                        Notice_ExpiredOn = c.DateTime(),
                        Notice_CreatedOn = c.DateTime(),
                        Notice_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Notice_Id);
            
            CreateTable(
                "dbo.AD_Newsletters",
                c => new
                    {
                        Newsletter_Id = c.Guid(nullable: false),
                        Newsletter_Email = c.String(),
                        Newsletter_Meet = c.Int(),
                        Newsletter_CreatedById = c.Guid(),
                        Newsletter_CreatedOn = c.DateTime(),
                        Newsletter_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Newsletter_Id)
                .ForeignKey("dbo.AD_Users", t => t.Newsletter_CreatedById)
                .Index(t => t.Newsletter_CreatedById);
            
            CreateTable(
                "dbo.AD_LogActivities",
                c => new
                    {
                        LogActivity_Id = c.Guid(nullable: false),
                        LogActivity_Comment = c.String(),
                        LogActivity_OperatedOn = c.DateTime(),
                        LogActivity_Url = c.String(),
                        LogActivity_Title = c.String(),
                        LogActivity_Agent = c.String(),
                        LogActivity_OperantIp = c.String(),
                        LogActivity_OperantedById = c.Guid(),
                        LogActivity_CreatedById = c.Guid(),
                        LogActivity_CreatedOn = c.DateTime(),
                        LogActivity_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.LogActivity_Id)
                .ForeignKey("dbo.AD_Users", t => t.LogActivity_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.LogActivity_OperantedById)
                .Index(t => t.LogActivity_OperantedById)
                .Index(t => t.LogActivity_CreatedById);
            
            CreateTable(
                "dbo.AD_LogAudits",
                c => new
                    {
                        LogAudit_Id = c.Guid(nullable: false),
                        LogAudit_CreatorIp = c.String(),
                        LogAudit_ModifierIp = c.String(),
                        LogAudit_IsModifyLock = c.Boolean(),
                        LogAudit_ModifiedOn = c.DateTime(),
                        LogAudit_ModifierAgent = c.String(),
                        LogAudit_CreatorAgent = c.String(),
                        LogAudit_Audit = c.Int(),
                        LogAudit_Description = c.String(),
                        LogAudit_OperatedOn = c.DateTime(),
                        LogAudit_Entity = c.String(),
                        LogAudit_XmlOldValue = c.String(),
                        LogAudit_XmlNewValue = c.String(),
                        LogAudit_EntityId = c.String(),
                        LogAudit_Agent = c.String(),
                        LogAudit_OperantIp = c.String(),
                        LogAudit_OperantedById = c.Guid(),
                        LogAudit_CreatedById = c.Guid(),
                        LogAudit_ModifiedById = c.Guid(),
                        LogAudit_CreatedOn = c.DateTime(),
                        LogAudit_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.LogAudit_Id)
                .ForeignKey("dbo.AD_Users", t => t.LogAudit_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.LogAudit_ModifiedById)
                .ForeignKey("dbo.AD_Users", t => t.LogAudit_OperantedById)
                .Index(t => t.LogAudit_OperantedById)
                .Index(t => t.LogAudit_CreatedById)
                .Index(t => t.LogAudit_ModifiedById);
            
            CreateTable(
                "dbo.AD_Emails",
                c => new
                    {
                        Email_Id = c.Guid(nullable: false),
                        Email_Title = c.String(),
                        Email_Body = c.String(),
                        Email_IsSend = c.Boolean(),
                        Email_SentById = c.Guid(),
                        Email_RecievedById = c.Guid(),
                        Email_CreatedOn = c.DateTime(),
                        Email_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Email_Id)
                .ForeignKey("dbo.AD_Users", t => t.Email_RecievedById)
                .ForeignKey("dbo.AD_Users", t => t.Email_SentById)
                .Index(t => t.Email_SentById)
                .Index(t => t.Email_RecievedById);
            
            CreateTable(
                "dbo.AD_Complaints",
                c => new
                    {
                        Complaint_Id = c.Guid(nullable: false),
                        Complaint_Body = c.String(),
                        Complaint_Title = c.String(),
                        Complaint_CreatedById = c.Guid(),
                        Complaint_CreatedOn = c.DateTime(),
                        Complaint_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.Complaint_Id)
                .ForeignKey("dbo.AD_Users", t => t.Complaint_CreatedById)
                .Index(t => t.Complaint_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyBalances",
                c => new
                    {
                        CompanyBalance_Id = c.Guid(nullable: false),
                        CompanyBalance_Amount = c.Int(),
                        CompanyBalance_TransactionedOn = c.DateTime(),
                        CompanyBalance_Description = c.String(),
                        CompanyBalance_IssueTracking = c.String(),
                        CompanyBalance_DocumentNumber = c.String(),
                        CompanyBalance_Depositor = c.String(),
                        CompanyBalance_CompanyId = c.Guid(),
                        CompanyBalance_SettingTransactionId = c.Guid(),
                        CompanyBalance_AttachmentFile = c.String(),
                        CompanyBalance_CreatedById = c.Guid(),
                        CompanyBalance_CreatedOn = c.DateTime(),
                        CompanyBalance_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyBalance_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyBalance_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyBalance_CreatedById)
                .ForeignKey("dbo.AD_SettingTransactions", t => t.CompanyBalance_SettingTransactionId)
                .Index(t => t.CompanyBalance_CompanyId)
                .Index(t => t.CompanyBalance_SettingTransactionId)
                .Index(t => t.CompanyBalance_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyConversations",
                c => new
                    {
                        CompanyConversation_Id = c.Guid(nullable: false),
                        CompanyConversation_Body = c.String(),
                        CompanyConversation_IsRead = c.Boolean(),
                        CompanyConversation_IsDeletedBySender = c.Boolean(),
                        CompanyConversation_IsDeletedByReceiver = c.Boolean(),
                        CompanyConversation_ReceivedById = c.Guid(),
                        CompanyConversation_ReplyId = c.Guid(),
                        CompanyConversation_CreatedById = c.Guid(),
                        CompanyConversation_CreatedOn = c.DateTime(),
                        CompanyConversation_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyConversation_Id)
                .ForeignKey("dbo.AD_CompanyConversations", t => t.CompanyConversation_ReplyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyConversation_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.CompanyConversation_ReceivedById)
                .Index(t => t.CompanyConversation_ReceivedById)
                .Index(t => t.CompanyConversation_ReplyId)
                .Index(t => t.CompanyConversation_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyRates",
                c => new
                    {
                        CompanyRate_Id = c.Guid(nullable: false),
                        CompanyRate_CompanyId = c.Guid(),
                        CompanyRate_CreatedById = c.Guid(),
                        CompanyRate_IsApprove = c.Boolean(),
                        CompanyRate_Rate = c.Int(),
                        CompanyRate_CreatedOn = c.DateTime(),
                        CompanyRate_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyRate_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyRate_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyRate_CreatedById)
                .Index(t => t.CompanyRate_CompanyId)
                .Index(t => t.CompanyRate_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyReserves",
                c => new
                    {
                        CompanyReserve_Id = c.Guid(nullable: false),
                        CompanyReserve_Alias = c.String(),
                        CompanyReserve_CreatedOn = c.DateTime(),
                        CompanyReserve_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyReserve_Id);
            
            CreateTable(
                "dbo.AD_CompanyTags",
                c => new
                    {
                        CompanyTag_Id = c.Guid(nullable: false),
                        CompanyTag_StartedOn = c.DateTime(),
                        CompanyTag_ExpiredOn = c.DateTime(),
                        CompanyTag_TagId = c.Guid(),
                        CompanyTag_CompanyId = c.Guid(),
                        CompanyTag_CreatedById = c.Guid(),
                        CompanyTag_CreatedOn = c.DateTime(),
                        CompanyTag_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyTag_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyTag_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyTag_CreatedById)
                .ForeignKey("dbo.AD_Tags", t => t.CompanyTag_TagId)
                .Index(t => t.CompanyTag_TagId)
                .Index(t => t.CompanyTag_CompanyId)
                .Index(t => t.CompanyTag_CreatedById);
            
            CreateTable(
                "dbo.AD_CompanyVideos",
                c => new
                    {
                        CompanyVideo_Id = c.Guid(nullable: false),
                        CompanyVideo_Title = c.String(),
                        CompanyVideo_Order = c.Int(),
                        CompanyVideo_State = c.Int(),
                        CompanyVideo_RejectDescription = c.String(),
                        CompanyVideo_CompanyId = c.Guid(),
                        CompanyVideo_ApprovedById = c.Guid(),
                        CompanyVideo_CreatedById = c.Guid(),
                        CompanyVideo_CreatedOn = c.DateTime(),
                        CompanyVideo_IsDelete = c.Boolean(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.CompanyVideo_Id)
                .ForeignKey("dbo.AD_Companies", t => t.CompanyVideo_CompanyId)
                .ForeignKey("dbo.AD_Users", t => t.CompanyVideo_CreatedById)
                .ForeignKey("dbo.AD_Users", t => t.User_Id)
                .Index(t => t.CompanyVideo_CompanyId)
                .Index(t => t.CompanyVideo_CreatedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AD_CompanyVideoFiles",
                c => new
                    {
                        CompanyVideoFile_Id = c.Guid(nullable: false),
                        CompanyVideoFile_Order = c.Int(),
                        CompanyVideoFile_IsWatermark = c.Boolean(),
                        CompanyVideoFile_WatermarkName = c.String(),
                        CompanyVideoFile_ThumbName = c.String(),
                        CompanyVideoFile_CompanyVideoId = c.Guid(),
                        CompanyVideoFile_Title = c.String(),
                        CompanyVideoFile_FileName = c.String(),
                        CompanyVideoFile_FileSize = c.String(),
                        CompanyVideoFile_FileExtension = c.String(),
                        CompanyVideoFile_FileDimension = c.String(),
                        CompanyVideoFile_FileResolution = c.String(),
                        CompanyVideoFile_CreatedOn = c.DateTime(),
                        CompanyVideoFile_IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CompanyVideoFile_Id)
                .ForeignKey("dbo.AD_CompanyVideos", t => t.CompanyVideoFile_CompanyVideoId)
                .Index(t => t.CompanyVideoFile_CompanyVideoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AD_CompanyVideoFiles", "CompanyVideoFile_CompanyVideoId", "dbo.AD_CompanyVideos");
            DropForeignKey("dbo.AD_CompanyVideos", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyVideos", "CompanyVideo_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyVideos", "CompanyVideo_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyTags", "CompanyTag_TagId", "dbo.AD_Tags");
            DropForeignKey("dbo.AD_CompanyTags", "CompanyTag_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyTags", "CompanyTag_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyRates", "CompanyRate_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyRates", "CompanyRate_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyConversations", "CompanyConversation_ReceivedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyConversations", "CompanyConversation_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyConversations", "CompanyConversation_ReplyId", "dbo.AD_CompanyConversations");
            DropForeignKey("dbo.AD_CompanyBalances", "CompanyBalance_SettingTransactionId", "dbo.AD_SettingTransactions");
            DropForeignKey("dbo.AD_CompanyBalances", "CompanyBalance_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyBalances", "CompanyBalance_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_Complaints", "Complaint_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Emails", "Email_SentById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Emails", "Email_RecievedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_LogAudits", "LogAudit_OperantedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_LogAudits", "LogAudit_ModifiedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_LogAudits", "LogAudit_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_LogActivities", "LogActivity_OperantedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_LogActivities", "LogActivity_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Newsletters", "Newsletter_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_PlanPayments", "PlanPayment_PlanDiscountId", "dbo.AD_PlanDiscounts");
            DropForeignKey("dbo.AD_PlanPayments", "PlanPayment_PlanId", "dbo.AD_Plans");
            DropForeignKey("dbo.AD_PlanPayments", "PlanPayment_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_PlanDiscounts", "PlanDiscount_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Plans", "Plan_RoleId", "dbo.AD_Roles");
            DropForeignKey("dbo.AD_Plans", "Plan_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductRates", "ProductRate_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductRates", "ProductRate_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductNotifies", "ProductNotify_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductNotifies", "ProductNotify_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Reports", "Report_ParentId", "dbo.AD_Reports");
            DropForeignKey("dbo.AD_Reports", "Report_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_SeoUrls", "SeoUrl_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Sms", "Sms_SentById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Sms", "Sms_RecievedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserOperators", "UserOperator_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserOnlines", "UserOnline_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserNotifications", "UserNotification_TargetId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_UserNotifications", "UserNotification_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserRoles", "UserRole_RoleId", "dbo.AD_Roles");
            DropForeignKey("dbo.AD_RolePermissions", "RolePermission_RoleId", "dbo.AD_Roles");
            DropForeignKey("dbo.AD_RolePermissions", "RolePermission_PermissionId", "dbo.AD_Permissions");
            DropForeignKey("dbo.AD_Permissions", "Permission_ParentId", "dbo.AD_Permissions");
            DropForeignKey("dbo.AD_Roles", "Role_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserSettings", "UserSetting_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserRoles", "UserRole_UserId", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserViolations", "UserViolation_ReportedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductCommentLikes", "ProductCommentLike_LikedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductCommentLikes", "ProductCommentLike_CommentId", "dbo.AD_ProductComments");
            DropForeignKey("dbo.AD_Products", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductComments", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Users", "User_MetaId", "dbo.AD_UserMetas");
            DropForeignKey("dbo.AD_UserLogins", "UserLogin_UserId", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Users", "User_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyVisits", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyQuestions", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Users", "User_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_UserClaims", "UserClaim_UserId", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserBudgets", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserBudgets", "UserBudget_PaymentId", "dbo.AD_ReceiptPayments");
            DropForeignKey("dbo.AD_ReceiptPayments", "ReceiptPayment_ReceiptId", "dbo.AD_Receipts");
            DropForeignKey("dbo.AD_ReceiptPayments", "ReceiptPayment_PayedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ReceiptPayments", "ReceiptPayment_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_AnnounceReserves", "AnnounceReserve_AnnounceId", "dbo.AD_Announces");
            DropForeignKey("dbo.AD_AnnouncePayments", "AnnouncePayment_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_AnnouncePayments", "AnnouncePayment_AnnounceId", "dbo.AD_Announces");
            DropForeignKey("dbo.AD_Announces", "Announce_OwnerId", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CategoryReviews", "CategoryReview_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CategoryReviews", "CategoryReview_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_CategoryFollows", "CategoryFollow_FollowedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CategoryFollows", "CategoryFollow_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_Categories", "Category_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Categories", "Category_ParentId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_Products", "CategoryOption_Id", "dbo.AD_CategoryOptions");
            DropForeignKey("dbo.AD_ProductVisits", "ProductVisit_VisitedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductVisits", "ProductVisit_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductTags", "ProductTag_TagId", "dbo.AD_Tags");
            DropForeignKey("dbo.AD_ProductTags", "ProductTag_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductReviews", "ProductReview_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductReviews", "ProductReview_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductReviews", "ProductReview_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Products", "Product_ManufacturerId", "dbo.AD_Manufacturers");
            DropForeignKey("dbo.AD_Products", "Product_LocationId", "dbo.AD_Locations");
            DropForeignKey("dbo.AD_ProductLikes", "ProductLike_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductLikes", "ProductLike_LikedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductImages", "ProductImage_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductImages", "ProductImage_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Products", "Product_GuaranteeId", "dbo.AD_Guarantees");
            DropForeignKey("dbo.AD_ProductFeatures", "ProductFeature_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_Products", "Product_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyVisits", "CompanyVisit_VisitedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyVisits", "CompanyVisit_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyVisits", "CompanyVisit_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanySocials", "CompanySocial_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanySocials", "CompanySocial_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanySlides", "CompanySlide_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_CompanySlides", "CompanySlide_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyReviews", "CompanyReview_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyReviews", "CompanyReview_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyQuestions", "CompanyQuestion_QuestionedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyQuestions", "CompanyQuestion_ModifiedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyQuestionLikes", "CompanyQuestionLike_QuestionId", "dbo.AD_CompanyQuestions");
            DropForeignKey("dbo.AD_CompanyQuestionLikes", "CompanyQuestionLike_LikedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyQuestions", "CompanyQuestion_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyQuestions", "CompanyQuestion_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyQuestions", "CompanyQuestion_ReplyId", "dbo.AD_CompanyQuestions");
            DropForeignKey("dbo.AD_CompanyQuestions", "CompanyQuestion_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Products", "Product_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_ReceiptOptions", "ReceiptOption_ReceiptId", "dbo.AD_Receipts");
            DropForeignKey("dbo.AD_ReceiptOptions", "ReceiptOption_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Receipts", "Receipt_LocationId", "dbo.AD_Locations");
            DropForeignKey("dbo.AD_Receipts", "Receipt_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_UserMetas", "UserMeta_LocationId", "dbo.AD_Locations");
            DropForeignKey("dbo.AD_UserMetas", "UserMeta_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Locations", "LocationCity_Id", "dbo.AD_LocationCities");
            DropForeignKey("dbo.AD_LocationCities", "LocationCity_ParentId", "dbo.AD_LocationCities");
            DropForeignKey("dbo.AD_Locations", "Location_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Companies", "Company_LocationId", "dbo.AD_Locations");
            DropForeignKey("dbo.AD_CompanyImages", "User_Id", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyImageFiles", "CompanyImageFile_CompanyImageId", "dbo.AD_CompanyImages");
            DropForeignKey("dbo.AD_CompanyImages", "CompanyImage_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyImages", "CompanyImage_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyFollows", "CompanyFollow_FollowedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyFollows", "CompanyFollow_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_Companies", "Company_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyOfficials", "CompanyOfficial_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyHours", "CompanyHour_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyHours", "CompanyHour_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_Companies", "Company_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_CompanyAttachments", "CompanyAttachment_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CompanyAttachments", "CompanyAttachment_CompanyId", "dbo.AD_Companies");
            DropForeignKey("dbo.AD_CompanyAttachmentFiles", "CompanyAttachmentFile_CompanyAttachmentId", "dbo.AD_CompanyAttachments");
            DropForeignKey("dbo.AD_CompanyAttachments", "CompanyAttachment_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Companies", "Company_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductComments", "ProductComment_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductComments", "ProductComment_CommentedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_ProductComments", "ProductComment_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Products", "Product_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_Products", "Product_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_CatalogSpecifications", "CatalogSpecification_SpecificationOptionId", "dbo.AD_SpecificationOptions");
            DropForeignKey("dbo.AD_CatalogSpecifications", "CatalogSpecification_SpecificationId", "dbo.AD_Specifications");
            DropForeignKey("dbo.AD_ProductSpecifications", "ProductSpecification_SpecificationOptionId", "dbo.AD_SpecificationOptions");
            DropForeignKey("dbo.AD_ProductSpecifications", "ProductSpecification_SpecificationId", "dbo.AD_Specifications");
            DropForeignKey("dbo.AD_ProductSpecifications", "ProductSpecification_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_SpecificationOptions", "SpecificationOption_SpecificationId", "dbo.AD_Specifications");
            DropForeignKey("dbo.AD_Specifications", "Specification_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_CatalogSpecifications", "CatalogSpecification_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_CatalogReviews", "CatalogReview_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_Catalogs", "Catalog_ManufacturerId", "dbo.AD_Manufacturers");
            DropForeignKey("dbo.AD_CatalogLikes", "CatalogLike_LikedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CatalogLikes", "CatalogLike_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_CatalogKeywords", "CatalogKeyword_KeywordId", "dbo.AD_Keywords");
            DropForeignKey("dbo.AD_ProductKeywords", "ProductKeyword_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_ProductKeywords", "ProductKeyword_KeywordId", "dbo.AD_Keywords");
            DropForeignKey("dbo.AD_Keywords", "Keyword_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_CatalogKeywords", "CatalogKeyword_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_CatalogImages", "CatalogImage_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_CatalogFeatures", "CatalogFeature_CatalogId", "dbo.AD_Catalogs");
            DropForeignKey("dbo.AD_Catalogs", "Catalog_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Catalogs", "Catalog_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_Carts", "Cart_ProductId", "dbo.AD_Products");
            DropForeignKey("dbo.AD_Carts", "Cart_CreatedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Products", "Product_ApprovedById", "dbo.AD_Users");
            DropForeignKey("dbo.AD_Categories", "Category_CategoryOptionId", "dbo.AD_CategoryOptions");
            DropForeignKey("dbo.AD_Announces", "Announce_CategoryId", "dbo.AD_Categories");
            DropForeignKey("dbo.AD_Announces", "Announce_AnnounceOptionId", "dbo.AD_AnnounceOptions");
            DropIndex("dbo.AD_CompanyVideoFiles", new[] { "CompanyVideoFile_CompanyVideoId" });
            DropIndex("dbo.AD_CompanyVideos", new[] { "User_Id" });
            DropIndex("dbo.AD_CompanyVideos", new[] { "CompanyVideo_CreatedById" });
            DropIndex("dbo.AD_CompanyVideos", new[] { "CompanyVideo_CompanyId" });
            DropIndex("dbo.AD_CompanyTags", new[] { "CompanyTag_CreatedById" });
            DropIndex("dbo.AD_CompanyTags", new[] { "CompanyTag_CompanyId" });
            DropIndex("dbo.AD_CompanyTags", new[] { "CompanyTag_TagId" });
            DropIndex("dbo.AD_CompanyRates", new[] { "CompanyRate_CreatedById" });
            DropIndex("dbo.AD_CompanyRates", new[] { "CompanyRate_CompanyId" });
            DropIndex("dbo.AD_CompanyConversations", new[] { "CompanyConversation_CreatedById" });
            DropIndex("dbo.AD_CompanyConversations", new[] { "CompanyConversation_ReplyId" });
            DropIndex("dbo.AD_CompanyConversations", new[] { "CompanyConversation_ReceivedById" });
            DropIndex("dbo.AD_CompanyBalances", new[] { "CompanyBalance_CreatedById" });
            DropIndex("dbo.AD_CompanyBalances", new[] { "CompanyBalance_SettingTransactionId" });
            DropIndex("dbo.AD_CompanyBalances", new[] { "CompanyBalance_CompanyId" });
            DropIndex("dbo.AD_Complaints", new[] { "Complaint_CreatedById" });
            DropIndex("dbo.AD_Emails", new[] { "Email_RecievedById" });
            DropIndex("dbo.AD_Emails", new[] { "Email_SentById" });
            DropIndex("dbo.AD_LogAudits", new[] { "LogAudit_ModifiedById" });
            DropIndex("dbo.AD_LogAudits", new[] { "LogAudit_CreatedById" });
            DropIndex("dbo.AD_LogAudits", new[] { "LogAudit_OperantedById" });
            DropIndex("dbo.AD_LogActivities", new[] { "LogActivity_CreatedById" });
            DropIndex("dbo.AD_LogActivities", new[] { "LogActivity_OperantedById" });
            DropIndex("dbo.AD_Newsletters", new[] { "Newsletter_CreatedById" });
            DropIndex("dbo.AD_PlanPayments", new[] { "PlanPayment_CreatedById" });
            DropIndex("dbo.AD_PlanPayments", new[] { "PlanPayment_PlanDiscountId" });
            DropIndex("dbo.AD_PlanPayments", new[] { "PlanPayment_PlanId" });
            DropIndex("dbo.AD_PlanDiscounts", new[] { "PlanDiscount_CreatedById" });
            DropIndex("dbo.AD_Plans", new[] { "Plan_CreatedById" });
            DropIndex("dbo.AD_Plans", new[] { "Plan_RoleId" });
            DropIndex("dbo.AD_ProductRates", new[] { "ProductRate_CreatedById" });
            DropIndex("dbo.AD_ProductRates", new[] { "ProductRate_ProductId" });
            DropIndex("dbo.AD_ProductNotifies", new[] { "ProductNotify_CreatedById" });
            DropIndex("dbo.AD_ProductNotifies", new[] { "ProductNotify_ProductId" });
            DropIndex("dbo.AD_Reports", new[] { "Report_ParentId" });
            DropIndex("dbo.AD_Reports", new[] { "Report_CreatedById" });
            DropIndex("dbo.AD_SeoUrls", new[] { "SeoUrl_CreatedById" });
            DropIndex("dbo.AD_Sms", new[] { "Sms_RecievedById" });
            DropIndex("dbo.AD_Sms", new[] { "Sms_SentById" });
            DropIndex("dbo.AD_UserOperators", new[] { "UserOperator_CreatedById" });
            DropIndex("dbo.AD_UserOnlines", new[] { "UserOnline_CreatedById" });
            DropIndex("dbo.AD_UserNotifications", new[] { "UserNotification_CreatedById" });
            DropIndex("dbo.AD_UserNotifications", new[] { "UserNotification_TargetId" });
            DropIndex("dbo.AD_Permissions", new[] { "Permission_ParentId" });
            DropIndex("dbo.AD_RolePermissions", new[] { "RolePermission_PermissionId" });
            DropIndex("dbo.AD_RolePermissions", new[] { "RolePermission_RoleId" });
            DropIndex("dbo.AD_UserSettings", new[] { "UserSetting_CreatedById" });
            DropIndex("dbo.AD_UserRoles", new[] { "UserRole_RoleId" });
            DropIndex("dbo.AD_UserRoles", new[] { "UserRole_UserId" });
            DropIndex("dbo.AD_UserViolations", new[] { "UserViolation_ReportedById" });
            DropIndex("dbo.AD_ProductCommentLikes", new[] { "ProductCommentLike_CommentId" });
            DropIndex("dbo.AD_ProductCommentLikes", new[] { "ProductCommentLike_LikedById" });
            DropIndex("dbo.AD_UserLogins", new[] { "UserLogin_UserId" });
            DropIndex("dbo.AD_UserClaims", new[] { "UserClaim_UserId" });
            DropIndex("dbo.AD_ReceiptPayments", new[] { "ReceiptPayment_CreatedById" });
            DropIndex("dbo.AD_ReceiptPayments", new[] { "ReceiptPayment_ReceiptId" });
            DropIndex("dbo.AD_ReceiptPayments", new[] { "ReceiptPayment_PayedById" });
            DropIndex("dbo.AD_UserBudgets", new[] { "User_Id" });
            DropIndex("dbo.AD_UserBudgets", new[] { "UserBudget_PaymentId" });
            DropIndex("dbo.AD_AnnounceReserves", new[] { "AnnounceReserve_AnnounceId" });
            DropIndex("dbo.AD_AnnouncePayments", new[] { "AnnouncePayment_CreatedById" });
            DropIndex("dbo.AD_AnnouncePayments", new[] { "AnnouncePayment_AnnounceId" });
            DropIndex("dbo.AD_CategoryReviews", new[] { "CategoryReview_CreatedById" });
            DropIndex("dbo.AD_CategoryReviews", new[] { "CategoryReview_CategoryId" });
            DropIndex("dbo.AD_CategoryFollows", new[] { "CategoryFollow_CategoryId" });
            DropIndex("dbo.AD_CategoryFollows", new[] { "CategoryFollow_FollowedById" });
            DropIndex("dbo.AD_ProductVisits", new[] { "ProductVisit_ProductId" });
            DropIndex("dbo.AD_ProductVisits", new[] { "ProductVisit_VisitedById" });
            DropIndex("dbo.AD_ProductTags", new[] { "ProductTag_ProductId" });
            DropIndex("dbo.AD_ProductTags", new[] { "ProductTag_TagId" });
            DropIndex("dbo.AD_ProductReviews", new[] { "ProductReview_CreatedById" });
            DropIndex("dbo.AD_ProductReviews", new[] { "ProductReview_ApprovedById" });
            DropIndex("dbo.AD_ProductReviews", new[] { "ProductReview_ProductId" });
            DropIndex("dbo.AD_ProductLikes", new[] { "ProductLike_ProductId" });
            DropIndex("dbo.AD_ProductLikes", new[] { "ProductLike_LikedById" });
            DropIndex("dbo.AD_ProductImages", new[] { "ProductImage_CreatedById" });
            DropIndex("dbo.AD_ProductImages", new[] { "ProductImage_ProductId" });
            DropIndex("dbo.AD_ProductFeatures", new[] { "ProductFeature_ProductId" });
            DropIndex("dbo.AD_CompanyVisits", new[] { "User_Id" });
            DropIndex("dbo.AD_CompanyVisits", new[] { "CompanyVisit_CreatedById" });
            DropIndex("dbo.AD_CompanyVisits", new[] { "CompanyVisit_CompanyId" });
            DropIndex("dbo.AD_CompanyVisits", new[] { "CompanyVisit_VisitedById" });
            DropIndex("dbo.AD_CompanySocials", new[] { "CompanySocial_CreatedById" });
            DropIndex("dbo.AD_CompanySocials", new[] { "CompanySocial_CompanyId" });
            DropIndex("dbo.AD_CompanySlides", new[] { "CompanySlide_ProductId" });
            DropIndex("dbo.AD_CompanySlides", new[] { "CompanySlide_CompanyId" });
            DropIndex("dbo.AD_CompanyReviews", new[] { "CompanyReview_ApprovedById" });
            DropIndex("dbo.AD_CompanyReviews", new[] { "CompanyReview_CompanyId" });
            DropIndex("dbo.AD_CompanyQuestionLikes", new[] { "CompanyQuestionLike_QuestionId" });
            DropIndex("dbo.AD_CompanyQuestionLikes", new[] { "CompanyQuestionLike_LikedById" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "User_Id" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "CompanyQuestion_ModifiedById" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "CompanyQuestion_CreatedById" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "CompanyQuestion_ApprovedById" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "CompanyQuestion_QuestionedById" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "CompanyQuestion_CompanyId" });
            DropIndex("dbo.AD_CompanyQuestions", new[] { "CompanyQuestion_ReplyId" });
            DropIndex("dbo.AD_ReceiptOptions", new[] { "ReceiptOption_CreatedById" });
            DropIndex("dbo.AD_ReceiptOptions", new[] { "ReceiptOption_ReceiptId" });
            DropIndex("dbo.AD_Receipts", new[] { "Receipt_CreatedById" });
            DropIndex("dbo.AD_Receipts", new[] { "Receipt_LocationId" });
            DropIndex("dbo.AD_UserMetas", new[] { "UserMeta_CreatedById" });
            DropIndex("dbo.AD_UserMetas", new[] { "UserMeta_LocationId" });
            DropIndex("dbo.AD_LocationCities", new[] { "LocationCity_ParentId" });
            DropIndex("dbo.AD_Locations", new[] { "LocationCity_Id" });
            DropIndex("dbo.AD_Locations", new[] { "Location_CreatedById" });
            DropIndex("dbo.AD_CompanyImageFiles", new[] { "CompanyImageFile_CompanyImageId" });
            DropIndex("dbo.AD_CompanyImages", new[] { "User_Id" });
            DropIndex("dbo.AD_CompanyImages", new[] { "CompanyImage_CreatedById" });
            DropIndex("dbo.AD_CompanyImages", new[] { "CompanyImage_CompanyId" });
            DropIndex("dbo.AD_CompanyFollows", new[] { "CompanyFollow_CompanyId" });
            DropIndex("dbo.AD_CompanyFollows", new[] { "CompanyFollow_FollowedById" });
            DropIndex("dbo.AD_CompanyOfficials", new[] { "CompanyOfficial_CompanyId" });
            DropIndex("dbo.AD_CompanyHours", new[] { "CompanyHour_CreatedById" });
            DropIndex("dbo.AD_CompanyHours", new[] { "CompanyHour_CompanyId" });
            DropIndex("dbo.AD_CompanyAttachmentFiles", new[] { "CompanyAttachmentFile_CompanyAttachmentId" });
            DropIndex("dbo.AD_CompanyAttachments", new[] { "CompanyAttachment_CreatedById" });
            DropIndex("dbo.AD_CompanyAttachments", new[] { "CompanyAttachment_ApprovedById" });
            DropIndex("dbo.AD_CompanyAttachments", new[] { "CompanyAttachment_CompanyId" });
            DropIndex("dbo.AD_Companies", new[] { "Company_CreatedById" });
            DropIndex("dbo.AD_Companies", new[] { "Company_CategoryId" });
            DropIndex("dbo.AD_Companies", new[] { "Company_LocationId" });
            DropIndex("dbo.AD_Companies", new[] { "Company_ApprovedById" });
            DropIndex("dbo.AD_ProductComments", new[] { "User_Id" });
            DropIndex("dbo.AD_ProductComments", new[] { "ProductComment_ProductId" });
            DropIndex("dbo.AD_ProductComments", new[] { "ProductComment_ApprovedById" });
            DropIndex("dbo.AD_ProductComments", new[] { "ProductComment_CommentedById" });
            DropIndex("dbo.AD_ProductSpecifications", new[] { "ProductSpecification_SpecificationOptionId" });
            DropIndex("dbo.AD_ProductSpecifications", new[] { "ProductSpecification_SpecificationId" });
            DropIndex("dbo.AD_ProductSpecifications", new[] { "ProductSpecification_ProductId" });
            DropIndex("dbo.AD_SpecificationOptions", new[] { "SpecificationOption_SpecificationId" });
            DropIndex("dbo.AD_Specifications", new[] { "Specification_CategoryId" });
            DropIndex("dbo.AD_CatalogSpecifications", new[] { "CatalogSpecification_SpecificationOptionId" });
            DropIndex("dbo.AD_CatalogSpecifications", new[] { "CatalogSpecification_SpecificationId" });
            DropIndex("dbo.AD_CatalogSpecifications", new[] { "CatalogSpecification_CatalogId" });
            DropIndex("dbo.AD_CatalogReviews", new[] { "CatalogReview_CatalogId" });
            DropIndex("dbo.AD_CatalogLikes", new[] { "CatalogLike_CatalogId" });
            DropIndex("dbo.AD_CatalogLikes", new[] { "CatalogLike_LikedById" });
            DropIndex("dbo.AD_ProductKeywords", new[] { "ProductKeyword_ProductId" });
            DropIndex("dbo.AD_ProductKeywords", new[] { "ProductKeyword_KeywordId" });
            DropIndex("dbo.AD_Keywords", new[] { "Keyword_CreatedById" });
            DropIndex("dbo.AD_CatalogKeywords", new[] { "CatalogKeyword_CatalogId" });
            DropIndex("dbo.AD_CatalogKeywords", new[] { "CatalogKeyword_KeywordId" });
            DropIndex("dbo.AD_CatalogImages", new[] { "CatalogImage_CatalogId" });
            DropIndex("dbo.AD_CatalogFeatures", new[] { "CatalogFeature_CatalogId" });
            DropIndex("dbo.AD_Catalogs", new[] { "Catalog_CreatedById" });
            DropIndex("dbo.AD_Catalogs", new[] { "Catalog_ManufacturerId" });
            DropIndex("dbo.AD_Catalogs", new[] { "Catalog_CategoryId" });
            DropIndex("dbo.AD_Carts", new[] { "Cart_CreatedById" });
            DropIndex("dbo.AD_Carts", new[] { "Cart_ProductId" });
            DropIndex("dbo.AD_Products", new[] { "User_Id" });
            DropIndex("dbo.AD_Products", new[] { "CategoryOption_Id" });
            DropIndex("dbo.AD_Products", new[] { "Product_ManufacturerId" });
            DropIndex("dbo.AD_Products", new[] { "Product_GuaranteeId" });
            DropIndex("dbo.AD_Products", new[] { "Product_CatalogId" });
            DropIndex("dbo.AD_Products", new[] { "Product_CreatedById" });
            DropIndex("dbo.AD_Products", new[] { "Product_CompanyId" });
            DropIndex("dbo.AD_Products", new[] { "Product_CategoryId" });
            DropIndex("dbo.AD_Products", new[] { "Product_LocationId" });
            DropIndex("dbo.AD_Products", new[] { "Product_ApprovedById" });
            DropIndex("dbo.AD_Categories", new[] { "Category_CreatedById" });
            DropIndex("dbo.AD_Categories", new[] { "Category_CategoryOptionId" });
            DropIndex("dbo.AD_Categories", new[] { "Category_ParentId" });
            DropIndex("dbo.AD_Announces", new[] { "Announce_CategoryId" });
            DropIndex("dbo.AD_Announces", new[] { "Announce_AnnounceOptionId" });
            DropIndex("dbo.AD_Announces", new[] { "Announce_OwnerId" });
            DropIndex("dbo.AD_Users", new[] { "User_CreatedById" });
            DropIndex("dbo.AD_Users", new[] { "User_CompanyId" });
            DropIndex("dbo.AD_Users", new[] { "User_MetaId" });
            DropIndex("dbo.AD_Roles", new[] { "Role_CreatedById" });
            DropTable("dbo.AD_CompanyVideoFiles");
            DropTable("dbo.AD_CompanyVideos");
            DropTable("dbo.AD_CompanyTags");
            DropTable("dbo.AD_CompanyReserves");
            DropTable("dbo.AD_CompanyRates");
            DropTable("dbo.AD_CompanyConversations");
            DropTable("dbo.AD_CompanyBalances");
            DropTable("dbo.AD_Complaints");
            DropTable("dbo.AD_Emails");
            DropTable("dbo.AD_LogAudits");
            DropTable("dbo.AD_LogActivities");
            DropTable("dbo.AD_Newsletters");
            DropTable("dbo.AD_Notices");
            DropTable("dbo.AD_PlanOptions");
            DropTable("dbo.AD_PlanPayments");
            DropTable("dbo.AD_PlanDiscounts");
            DropTable("dbo.AD_Plans");
            DropTable("dbo.AD_ProductVideos");
            DropTable("dbo.AD_ProductRates");
            DropTable("dbo.AD_ProductNotifies");
            DropTable("dbo.AD_Reports");
            DropTable("dbo.AD_SeoUrls");
            DropTable("dbo.AD_SeoSettings");
            DropTable("dbo.AD_Seos");
            DropTable("dbo.AD_SettingTransactions");
            DropTable("dbo.AD_Settings");
            DropTable("dbo.AD_SmsOperators");
            DropTable("dbo.AD_Sms");
            DropTable("dbo.AD_Statistics");
            DropTable("dbo.AD_UserOperators");
            DropTable("dbo.AD_UserOnlines");
            DropTable("dbo.AD_UserNotifications");
            DropTable("dbo.AD_Permissions");
            DropTable("dbo.AD_RolePermissions");
            DropTable("dbo.AD_UserSettings");
            DropTable("dbo.AD_UserRoles");
            DropTable("dbo.AD_UserViolations");
            DropTable("dbo.AD_ProductCommentLikes");
            DropTable("dbo.AD_UserLogins");
            DropTable("dbo.AD_UserClaims");
            DropTable("dbo.AD_ReceiptPayments");
            DropTable("dbo.AD_UserBudgets");
            DropTable("dbo.AD_AnnounceReserves");
            DropTable("dbo.AD_AnnouncePayments");
            DropTable("dbo.AD_CategoryReviews");
            DropTable("dbo.AD_CategoryFollows");
            DropTable("dbo.AD_ProductVisits");
            DropTable("dbo.AD_Tags");
            DropTable("dbo.AD_ProductTags");
            DropTable("dbo.AD_ProductReviews");
            DropTable("dbo.AD_ProductLikes");
            DropTable("dbo.AD_ProductImages");
            DropTable("dbo.AD_Guarantees");
            DropTable("dbo.AD_ProductFeatures");
            DropTable("dbo.AD_CompanyVisits");
            DropTable("dbo.AD_CompanySocials");
            DropTable("dbo.AD_CompanySlides");
            DropTable("dbo.AD_CompanyReviews");
            DropTable("dbo.AD_CompanyQuestionLikes");
            DropTable("dbo.AD_CompanyQuestions");
            DropTable("dbo.AD_ReceiptOptions");
            DropTable("dbo.AD_Receipts");
            DropTable("dbo.AD_UserMetas");
            DropTable("dbo.AD_LocationCities");
            DropTable("dbo.AD_Locations");
            DropTable("dbo.AD_CompanyImageFiles");
            DropTable("dbo.AD_CompanyImages");
            DropTable("dbo.AD_CompanyFollows");
            DropTable("dbo.AD_CompanyOfficials");
            DropTable("dbo.AD_CompanyHours");
            DropTable("dbo.AD_CompanyAttachmentFiles");
            DropTable("dbo.AD_CompanyAttachments");
            DropTable("dbo.AD_Companies");
            DropTable("dbo.AD_ProductComments");
            DropTable("dbo.AD_ProductSpecifications");
            DropTable("dbo.AD_SpecificationOptions");
            DropTable("dbo.AD_Specifications");
            DropTable("dbo.AD_CatalogSpecifications");
            DropTable("dbo.AD_CatalogReviews");
            DropTable("dbo.AD_Manufacturers");
            DropTable("dbo.AD_CatalogLikes");
            DropTable("dbo.AD_ProductKeywords");
            DropTable("dbo.AD_Keywords");
            DropTable("dbo.AD_CatalogKeywords");
            DropTable("dbo.AD_CatalogImages");
            DropTable("dbo.AD_CatalogFeatures");
            DropTable("dbo.AD_Catalogs");
            DropTable("dbo.AD_Carts");
            DropTable("dbo.AD_Products");
            DropTable("dbo.AD_CategoryOptions");
            DropTable("dbo.AD_Categories");
            DropTable("dbo.AD_AnnounceOptions");
            DropTable("dbo.AD_Announces");
            DropTable("dbo.AD_Users");
            DropTable("dbo.AD_Roles");
        }
    }
}
