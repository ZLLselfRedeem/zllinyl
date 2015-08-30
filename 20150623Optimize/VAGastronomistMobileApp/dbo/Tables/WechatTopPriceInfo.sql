CREATE TABLE [dbo].[WechatTopPriceInfo] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [msgContent]  NVARCHAR (1000) NULL,
    [pubDateTime] NVARCHAR (50)   NULL,
    [operaterID]  INT             NULL,
    [status]      NVARCHAR (50)   NULL,
    CONSTRAINT [PK_WechatTopPriceInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);

