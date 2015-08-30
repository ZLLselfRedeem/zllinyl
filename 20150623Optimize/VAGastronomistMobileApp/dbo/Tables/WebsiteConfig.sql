CREATE TABLE [dbo].[WebsiteConfig] (
    [id]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [type]       INT             NULL,
    [title]      NVARCHAR (100)  NULL,
    [date]       NVARCHAR (50)   NULL,
    [content]    NVARCHAR (4000) NULL,
    [sequence]   INT             NULL,
    [imageName]  NVARCHAR (100)  NULL,
    [updateTime] DATETIME        NULL,
    [status]     INT             NULL,
    [remark]     NVARCHAR (500)  NULL,
    CONSTRAINT [PK_WebsiteConfig] PRIMARY KEY CLUSTERED ([id] ASC)
);

