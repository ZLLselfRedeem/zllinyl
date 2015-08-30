CREATE TABLE [dbo].[WechatPayOrderInfo] (
    [outTradeNo]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [totalFee]            FLOAT (53)     NULL,
    [orderCreateTime]     DATETIME       NULL,
    [orderPayTime]        DATETIME       NULL,
    [orderStatus]         INT            NULL,
    [conn19dianOrderType] INT            NULL,
    [connId]              BIGINT         NULL,
    [body]                NVARCHAR (128) NULL,
    [wechatPrePayId]      NVARCHAR (32)  NULL,
    [customerId]          BIGINT         NULL,
    [openId]              NVARCHAR (100) NULL,
    [bankType]            NVARCHAR (16)  NULL,
    [bankBillno]          NVARCHAR (32)  NULL,
    [notifyId]            NVARCHAR (128) NULL,
    [transactionId]       NVARCHAR (28)  NULL,
    CONSTRAINT [PK_WcpayOrderInfo] PRIMARY KEY CLUSTERED ([outTradeNo] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_wechatPayOrderInfo_connId]
    ON [dbo].[WechatPayOrderInfo]([connId] ASC);

