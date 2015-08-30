CREATE TABLE [dbo].[WechatUxianQandAInfo] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [question]    NVARCHAR (500) NULL,
    [answer]      NVARCHAR (500) NULL,
    [pubDateTime] NVARCHAR (50)  NULL,
    [operaterID]  INT            NULL,
    CONSTRAINT [PK_WechatUxianQandAInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);

