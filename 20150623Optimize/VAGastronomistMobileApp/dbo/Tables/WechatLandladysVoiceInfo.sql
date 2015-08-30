CREATE TABLE [dbo].[WechatLandladysVoiceInfo] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [fileName]    NVARCHAR (500) NULL,
    [remark]      NVARCHAR (500) NULL,
    [pubDateTime] NVARCHAR (50)  NULL,
    [operaterID]  INT            NULL,
    [media_id]    NVARCHAR (100) NULL,
    [status]      NVARCHAR (10)  NULL,
    CONSTRAINT [PK_WechatLandladysVoiceInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);

