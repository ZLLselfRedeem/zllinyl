CREATE TABLE [dbo].[WechatCustomerInfo] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [subscribe] NVARCHAR (10)  NULL,
    [openId]    NVARCHAR (500) NULL,
    [nickName]  NVARCHAR (100) NULL,
    [sex]       NVARCHAR (10)  NULL,
    [language]  NVARCHAR (50)  NULL,
    [city]      NVARCHAR (50)  NULL,
    [province]  NVARCHAR (50)  NULL,
    [country]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_WechatCustomerInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);

