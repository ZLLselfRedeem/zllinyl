CREATE TABLE [dbo].[WechatAccessToken] (
    [id]          INT            NOT NULL,
    [accessToken] NVARCHAR (500) NULL,
    [createtime]  DATETIME       NULL,
    [expireTime]  DATETIME       NULL,
    [valid]       INT            NULL,
    CONSTRAINT [PK_WechatAccessToken] PRIMARY KEY CLUSTERED ([id] ASC)
);

