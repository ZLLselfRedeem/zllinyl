CREATE TABLE [dbo].[AliRefundOrderInfo] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [batchNo]        NVARCHAR (100) NOT NULL,
    [refundDate]     DATETIME       NOT NULL,
    [batchNum]       INT            NOT NULL,
    [aliTradeNo]     NVARCHAR (400) NOT NULL,
    [refundSum]      FLOAT (53)     NOT NULL,
    [refundReason]   NVARCHAR (100) NOT NULL,
    [refundStatus]   INT            NOT NULL,
    [notifyTime]     NVARCHAR (50)  NULL,
    [notifyType]     NVARCHAR (50)  NULL,
    [notifyId]       NVARCHAR (50)  NULL,
    [successNum]     NVARCHAR (50)  NULL,
    [connId]         BIGINT         NOT NULL,
    [lastUpdateTime] DATETIME       NULL,
    [originalId]     BIGINT         NULL,
    [customerId]     BIGINT         NULL,
    [notifyStatus]   INT            NULL,
    CONSTRAINT [PK_AliRefundOrderInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);

