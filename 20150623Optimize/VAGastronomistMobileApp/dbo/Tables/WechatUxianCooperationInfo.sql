CREATE TABLE [dbo].[WechatUxianCooperationInfo] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [msgContent]  NVARCHAR (500) NULL,
    [pubDateTime] NVARCHAR (50)  NULL,
    [operaterID]  INT            NULL,
    CONSTRAINT [PK_WechatUxianCooperationInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);

