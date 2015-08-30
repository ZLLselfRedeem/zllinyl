CREATE TABLE [dbo].[RefundLogData] (
    [id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [customerId]       BIGINT        NULL,
    [refundSum]        FLOAT (53)    NULL,
    [preOrder19dianId] BIGINT        NULL,
    [refundTime]       DATETIME      NULL,
    [note]             NVARCHAR (50) NULL,
    CONSTRAINT [PK_RefundLogData] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_RefundLogData_orderId]
    ON [dbo].[RefundLogData]([preOrder19dianId] ASC);

