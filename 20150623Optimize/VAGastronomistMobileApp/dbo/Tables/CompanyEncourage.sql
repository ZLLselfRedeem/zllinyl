CREATE TABLE [dbo].[CompanyEncourage] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [type]        INT            NULL,
    [value]       NVARCHAR (50)  NULL,
    [reason]      NVARCHAR (50)  NULL,
    [description] NVARCHAR (500) NULL,
    [createTime]  DATETIME       NULL,
    [creater]     INT            NULL,
    [status]      INT            NULL,
    [companyId]   INT            NULL,
    CONSTRAINT [PK_CompanyEncourage] PRIMARY KEY CLUSTERED ([id] ASC)
);

