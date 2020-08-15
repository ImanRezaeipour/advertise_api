
INSERT INTO [dbo].[AD_Users]
([User_Id]
,[User_IsBan]
,[User_BannedReason]
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
,[User_ModifiedOn]
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
,User_IsModifyLock
,User_IsDelete
,User_Version
,User_Action
,User_CreatedById)
     VALUES
 (N'9d2b0228-4d0d-4c23-8b49-01a698857709'
 ,0
 ,''
 ,GETDATE ()
 ,1
 ,1
 ,1
 ,''
 ,''
 ,GETDATE ()
 ,GETDATE ()
 ,1
 ,''
 ,1
 ,''
 ,'2016-12-12'
  ,'2016-12-12'
 ,'info@admin.com'
 ,1
 ,'ANSAvGmjnXw+WC9JrkqCvntY5l3L4f5NeYsglZATKmy51u52vopygV7pHE7fR7CXtQ=='
 ,'e0aa5980-a156-47ea-b03f-024f60fa02e1'
 ,''
 ,1
 ,1
 ,''
 ,1
 ,1
 ,'Admin'
 ,0
 ,0
 ,1
 ,1
 ,N'9d2b0228-4d0d-4c23-8b49-01a698857709')
GO








USE [AdvertiseDB]
GO




