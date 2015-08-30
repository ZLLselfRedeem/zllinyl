CREATE TABLE [dbo].[RedEnvelopeRankList] (
    [id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [mobilePhoneNumber] NVARCHAR (20) NULL,
    [ranking]           BIGINT        NULL,
    [createTime]        DATETIME      NULL,
    [lastUpdateTime]    DATETIME      NULL,
    [rankState]         INT           NULL,
    [amount]            FLOAT (53)    NULL,
    CONSTRAINT [PK_RedEnvelopeRankList] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ix_rankList_mobile]
    ON [dbo].[RedEnvelopeRankList]([mobilePhoneNumber] ASC);

