INSERT INTO [dbo].[AD_UserMetas]
([UserMeta_Id]
,[UserMeta_Code]
,[UserMeta_FirstName]
,[UserMeta_LastName]
,[UserMeta_DisplayName]
,[UserMeta_NationalCode]
,[UserMeta_BirthOn]
,[UserMeta_MarriedOn]
,[UserMeta_AvatarFileName]
,[UserMeta_IsActive]
,[UserMeta_Gender]
,[UserMeta_AboutMe]
,[UserMeta_AddressId]
,[UserMeta_CreatedOn]
,[UserMeta_ModifiedOn]
,[UserMeta_CreatorIp]
,[UserMeta_ModifierIp]
,[UserMeta_IsModifyLock]
,[UserMeta_IsDelete]
,[UserMeta_ModifierAgent]
,[UserMeta_CreatorAgent]
,[UserMeta_Version]
,[UserMeta_Audit]
,[UserMeta_ModifiedById]
,[UserMeta_CreatedById])
     VALUES
(N'9D2B0228-4D0D-4C23-8B49-01A698857710'
,''
,''
,''
,''
,''
,GETDATE ()
,GETDATE ()
,''
,1
,1
,''
,NULL
,GETDATE ()
,GETDATE ()
,''
,''
,0
,0
,''
,''
,1
,1
,N'9D2B0228-4D0D-4C23-8B49-01A698857709'
,N'9D2B0228-4D0D-4C23-8B49-01A698857709')
GO