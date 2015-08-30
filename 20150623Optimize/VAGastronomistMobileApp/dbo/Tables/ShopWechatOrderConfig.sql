CREATE TABLE [dbo].[ShopWechatOrderConfig] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [cookie]         NVARCHAR (100) NULL,
    [shopId]         INT            NULL,
    [status]         INT            NULL,
    [createdTime]    DATETIME       NULL,
    [wechatOrderUrl] NVARCHAR (200) NULL,
    CONSTRAINT [PK_ShopWechatOrderConfig] PRIMARY KEY CLUSTERED ([id] ASC)
);

