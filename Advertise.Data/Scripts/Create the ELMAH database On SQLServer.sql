
/* ------------------------------------------------------------------------ 
        STORED PROCEDURES                                                      
   ------------------------------------------------------------------------ */

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [ExceptionLog_AllXml]
    FROM 
        [AD_ExceptionLogs]
    WHERE
        [ExceptionLog_ErrorId] = @ErrorId
    AND
        [ExceptionLog_Application] = @Application

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [AD_ExceptionLogs]
    WHERE 
        [ExceptionLog_Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [ExceptionLog_TimeUtc],
            @FirstSequence = [ExceptionLog_Sequence]
        FROM 
            [AD_ExceptionLogs]
        WHERE   
            [ExceptionLog_Application] = @Application
        ORDER BY 
            [ExceptionLog_TimeUtc] DESC, 
            [ExceptionLog_Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ExceptionLog_ErrorId], 
        application = [ExceptionLog_Application],
        host        = [ExceptionLog_Host], 
        type        = [ExceptionLog_Type],
        source      = [ExceptionLog_Source],
        message     = [ExceptionLog_Message],
        [user]      = [ExceptionLog_User],
        statusCode  = [ExceptionLog_StatusCode], 
        time        = CONVERT(VARCHAR(50), [ExceptionLog_TimeUtc], 126) + 'Z'
    FROM 
        [AD_ExceptionLogs] error
    WHERE
        [ExceptionLog_Application] = @Application
    AND
        [ExceptionLog_TimeUtc] <= @FirstTimeUTC
    AND 
        [ExceptionLog_Sequence] <= @FirstSequence
    ORDER BY
        [ExceptionLog_TimeUtc] DESC, 
        [ExceptionLog_Sequence] DESC
    FOR
        XML AUTO

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [AD_ExceptionLogs]
        (
			[ExceptionLog_Id],
            [ExceptionLog_ErrorId],
            [ExceptionLog_Application],
            [ExceptionLog_Host],
            [ExceptionLog_Type],
            [ExceptionLog_Source],
            [ExceptionLog_Message],
            [ExceptionLog_User],
            [ExceptionLog_AllXml],
            [ExceptionLog_StatusCode],
            [ExceptionLog_TimeUtc]
        )
    VALUES
        (
			NEWID(),
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

