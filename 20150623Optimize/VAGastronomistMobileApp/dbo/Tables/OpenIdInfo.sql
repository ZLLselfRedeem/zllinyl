CREATE TABLE [dbo].[OpenIdInfo] (
    [id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [customerId]       BIGINT         NULL,
    [openIdUid]        NVARCHAR (100) NULL,
    [openIdBindTime]   DATETIME       NULL,
    [expirationDate]   DATETIME       NULL,
    [openIdType]       INT            NULL,
    [openIdUpdateTime] DATETIME       NULL,
    [openIdSession]    NVARCHAR (100) NULL,
    CONSTRAINT [PK_OpenIdInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

