CREATE TABLE [dbo].[WechatProposalReceiveInfo] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [msgContent]      NVARCHAR (500) NULL,
    [contentType]     INT            NULL,
    [senderWechatID]  NVARCHAR (100) NULL,
    [status]          NVARCHAR (50)  NULL,
    [receiveDateTime] NVARCHAR (50)  NULL,
    [voicefileName]   NVARCHAR (500) NULL,
    [operaterID]      INT            NULL,
    [operateDateTime] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_WechatProposalReceiveInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);

