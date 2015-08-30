CREATE TABLE [dbo].[AlipayOrderInfo] (
    [alipayOrderId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [totalFee]            FLOAT (53)     NULL,
    [orderCreatTime]      DATETIME       NULL,
    [orderPayTime]        DATETIME       NULL,
    [orderStatus]         INT            NULL,
    [conn19dianOrderType] INT            NULL,
    [connId]              BIGINT         NULL,
    [subject]             NVARCHAR (500) NULL,
    [aliTradeNo]          NVARCHAR (32)  NULL,
    [aliBuyerEmail]       NVARCHAR (100) NULL,
    [customerId]          BIGINT         NULL,
    CONSTRAINT [PK_AlipayOrderInfo] PRIMARY KEY CLUSTERED ([alipayOrderId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_alipayOrderInfo_connId]
    ON [dbo].[AlipayOrderInfo]([connId] ASC);

