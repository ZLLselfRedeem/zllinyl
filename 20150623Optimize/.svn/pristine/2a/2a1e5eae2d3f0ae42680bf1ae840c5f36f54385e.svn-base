CREATE TABLE [dbo].[MoneyRefundDetail] (
    [RefundId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [preOrder19dianId] BIGINT         NULL,
    [refundMoney]      FLOAT (53)     NULL,
    [remark]           NVARCHAR (500) NULL,
    [operUser]         VARCHAR (100)  NULL,
    [operTime]         DATETIME       NULL,
    CONSTRAINT [PK_MoneyRefundDetail] PRIMARY KEY CLUSTERED ([RefundId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点单Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyRefundDetail', @level2type = N'COLUMN', @level2name = N'preOrder19dianId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'退款金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyRefundDetail', @level2type = N'COLUMN', @level2name = N'refundMoney';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'退款原因', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyRefundDetail', @level2type = N'COLUMN', @level2name = N'remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作人员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyRefundDetail', @level2type = N'COLUMN', @level2name = N'operUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'MoneyRefundDetail', @level2type = N'COLUMN', @level2name = N'operTime';

