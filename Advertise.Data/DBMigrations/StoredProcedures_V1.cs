using System.Data.Entity.Migrations;

namespace Advertise.Data.DbMigrations
{
    public class StoredProcedures_V1 : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("dbo.InitUserTable", @"
CREATE PROCEDURE dbo.InitUserTable 
AS
INSERT INTO [dbo].[AD_Users]
           (
		   [User_Id]
           ,[User_IsBan]
           ,[User_BannedReason]
           ,[User_Code]
           ,[User_BannedOn]
           ,[User_IsVerify]
           ,[User_IsActive]
           ,[User_IsAnonymous]
           ,[User_EmailConfirmationToken]
           ,[User_MobileConfirmationToken]
           ,[User_LastPasswordChangedOn]
           ,[User_LastLoginedOn]
           ,[User_IsSystemAccount]
           ,[User_LastIp]
           ,[User_IsChangePermission]
           ,[User_DirectPermissions]
           ,[User_CreatedOn]
           ,[User_IsDelete]
           ,[User_Version]
           ,[User_MetaId]
           ,[User_CompanyId]
           ,[User_CreatedById]
           ,[User_Email]
           ,[User_EmailConfirmed]
           ,[User_PasswordHash]
           ,[User_SecurityStamp]
           ,[User_PhoneNumber]
           ,[User_PhoneNumberConfirmed]
           ,[User_TwoFactorEnabled]
           ,[User_LockoutEndDateUtc]
           ,[User_LockoutEnabled]
           ,[User_AccessFailedCount]
           ,[User_UserName]
		   )
     VALUES
           (
		   '11111111-1111-1111-1111-111111111111' --[User_Id]
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,1
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,NULL
           ,0
           ,NULL
           ,NULL
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,'11111111-1111-1111-1111-111111111111' --[User_CreatedById]
           ,NULL
           ,1
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,0
           ,NULL
           ,0
           ,0 --[User_AccessFailedCount]
           ,'System Admin' --[User_UserName]
		   )
GO
RETURN");
            
            CreateStoredProcedure("dbo.InitRoleTable", @"
CREATE PROCEDURE dbo.InitRoleTable 
AS
INSERT INTO [dbo].[AD_Roles]
       ([Role_Id]
       ,[Role_Code]
       ,[Role_IsSystemRole]
       ,[Role_IsBan]
       ,[Role_Permissions]
       ,[Role_CreatedOn]
       ,[Role_ModifiedOn]
       ,[Role_CreatorIp]
       ,[Role_ModifierIp]
       ,[Role_IsModifyLock]
       ,[Role_IsDelete]
       ,[Role_ModifierAgent]
       ,[Role_CreatorAgent]
       ,[Role_Version]
       ,[Role_Audit]
       ,[Role_CreatedById]
       ,[Role_Name])
VALUES
      ('36BDAC5D-BFC6-4F70-9C07-89C614AE0F97'
      ,'1'
      ,0
      ,0
      ,NULL
      ,NULL
      ,NULL
      ,NULL
      ,NULL
      ,0
      ,0
      ,NULL
      ,NULL
      ,0
      ,NULL
      ,'11111111-1111-1111-1111-111111111111'
      ,'Users')
GO
RETURN");
            
            CreateStoredProcedure("dbo.InitCategoryTable", @"
CREATE PROCEDURE dbo.InitCategoryTable  
AS
INSERT INTO [dbo].[AD_Categories]
       ([Category_Id]
       ,[Category_Code]
       ,[Category_Alias]
       ,[Category_Order]
       ,[Category_Title]
       ,[Category_Description]
       ,[Category_ImageFileName]
       ,[Category_HasChild]
       ,[Category_IsActive]
       ,[Category_Type]
       ,[Category_MetaTitle]
       ,[Category_MetaKeywords]
       ,[Category_MetaDescription]
       ,[Category_Level]
       ,[Category_ParentId]
       ,[Category_CategoryOptionId]
       ,[Category_CreatedById]
       ,[Category_CreatedOn]
       ,[Category_IsDelete])
VALUES
      ('E866996A-D5D9-41B6-AAED-EA36324190D5'
      ,'1'
      ,'Root'
      ,0
      ,'Root'
      ,NULL
      ,NULL
      ,1
      ,1
      ,0
      ,NULL
      ,NULL
      ,NULL
      ,0
      ,NULL
      ,NULL
      ,'11111111-1111-1111-1111-111111111111'
      ,NULL
      ,0)
GO
RETURN
");
            
            CreateStoredProcedure("dbo.InitLocationCityTable", @"
CREATE PROCEDURE dbo.InitLocationCityTable 
AS
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761596', 0, N'استانها',				1,  NULL,									 CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, NULL,				NULL)
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761597', 0, N'اردبیل',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۸٫۲۴۶۴۷۱',	N'۴۸٫۲۹۵۰۵۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761598', 0, N'اصفهان',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۲٫۶۵۱۳۹',		N'۵۱٫۶۷۹۱۹۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761599', 0, N'البرز',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۵٫۸۱۲۲۷۵',	N'۵۱٫۰۰۷۴۶۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761600', 0, N'ایلام',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۳٫۶۳۸۵۳۱',	N'۴۶٫۴۲۲۶۴۹')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761601', 0, N'آذربایجان شرقی	',		1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۸٫۰۵۲۴۶۷۵',	N'۴۶٫۲۸۴۹۹۲۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761602', 0, N'آذربایجان غربی	',		1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۷٫۵۲۹۶۰۷۱',	N'۴۵٫۰۴۶۵۴۸۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761603', 0, N'بوشهر',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۲۸٫۹۲۲۰۴۱',	N'۵۰٫۸۳۳۰۹۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761604', 0, N'تهران',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۵٫۴۱۵۹۰۳',	N'۵۱٫۲۰۱۶۹۸')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761605', 0, N'خراسان جنوبی',			1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۲٫۸۸۰۲۹۹',	N'۵۹٫۲۱۰۴۱۱')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761606', 0, N'خراسان رضوی	',		1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۶٫۱۷۱۶۵۹',	N'۵۹٫۳۶۵۶۷۵')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761607', 0, N'خراسان شمالی',			1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۷٫۴۵۲۴۳۸۳',	N'۵۷٫۳۲۳۵۱۷۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761608', 0, N'خوزستان',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۱٫۳۰۳۱۰۴۴',	N'۴۸٫۶۵۶۴۳۳۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761609', 0, N'زنجان',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۶٫۶۷۰۹۴',		N'۴۸٫۴۸۵۱۱۱')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761610', 0, N'سمنان',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۵٫۵۷۲۲۶۹',	N'۵۳٫۳۹۶۰۴۹')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761611', 0, N'سیستان و بلوچستان',	1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۲۹٫۴۵۴۵۷۸۶',	N'۶۰٫۸۵۳۶۴۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761612', 0, N'استان فارس',			1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'29.617248',		N'52.543423')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761613', 0, N'قزوین',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۶٫۲۶۶۸۱۹',	N'۵۰٫۰۰۳۸۱۱')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761614', 0, N'قم',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۴٫۶۴۳۷۱۱',	N'۵۰٫۸۹۰۶۴')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761615', 0, N'کردستان',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۵٫۳۰۲۹۴۲۲',	N'۴۷٫۰۰۲۶۳۱۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761616', 0, N'کرمان',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۰٫۲۸۰۲۷',		N'۵۷٫۰۶۷۰۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761617', 0, N'کرمانشاه',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۴٫۳۰۸۱۵۹',	N'۴۷٫۰۵۷۳۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761618', 0, N'کهگیلویه و بویراحمد',	1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'',				N'')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761619', 0, N'گلستان',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۶٫۸۳۶۵۷۱',	N'۵۴٫۴۳۲۶۲۹')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761620', 0, N'گیلان',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۷٫۲۲۳۴۳۱',	N'۴۹٫۶۳۵۵۰۹۱')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761621', 0, N'لرستان',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۳٫۴۶۶۶۶۶۷',	N'۴۸٫۳۵')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761622', 0, N'مازندران',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۶٫۵۸۰۳۷۵۸',	N'۵۳٫۰۵۹۸۵۵۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761623', 0, N'مرکزی',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۴٫۰۸۰۰۱۱۲',	N'۴۹٫۶۷۷۲۳۳۴')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761624', 0, N'هرمزگان',				1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۲۷٫۱۸۳۳۳۳۳',	N'۵۶٫۲۶۶۶۶۶۷')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761625', 0, N'همدان',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۴٫۸۰۶۵',		N'۴۸٫۵۱۶۲۴۷۲')
INSERT [dbo].[AD_LocationCities] ([LocationCity_Id], [LocationCity_IsState], [LocationCity_Name], [LocationCity_IsActive], [LocationCity_ParentId], [LocationCity_CreatedOn], [LocationCity_IsDelete], [LocationCity_latitude], [LocationCity_longitude]) VALUES (N'2af15795-00ad-60ad-15c6-87623d761626', 0, N'یزد',					1,  N'2af15795-00ad-60ad-15c6-87623d761596', CAST(N'2017-01-19 20:05:54.640' AS DateTime), 0, N'۳۱٫۸۹۶۶۱',		N'۵۴٫۳۶۰۶۸')
RETURN");
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.InitUserTable");
        }
    }
}