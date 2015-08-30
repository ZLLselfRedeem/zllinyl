CREATE TABLE [dbo].[TenpayRefundOrder] (
    [Id]                       BIGINT        IDENTITY (1, 1) NOT NULL,
    [OutRefundId]              VARCHAR (32)  NOT NULL,
    [OutTradeNo]               BIGINT        NOT NULL,
    [WechatPrePayId]           VARCHAR (28)  NULL,
    [RefundId]                 VARCHAR (28)  NULL,
    [RefundFee]                FLOAT (53)    NOT NULL,
    [RefundChannel]            INT           NOT NULL,
    [OpUserId]                 VARCHAR (20)  NULL,
    [RecvUserId]               NVARCHAR (64) NULL,
    [ReccvUserName]            NVARCHAR (32) NULL,
    [Status]                   INT           NOT NULL,
    [CretaeTime]               DATETIME      DEFAULT (getdate()) NOT NULL,
    [ChangeStatusTime]         DATETIME      NULL,
    [preOrder19dianId]         BIGINT        NULL,
    [OriginalRoadRefundInfoId] BIGINT        NULL
);

