CREATE TABLE [dbo].[CouponSendDetail] (
    [CouponSendDetailID] BIGINT   IDENTITY (1, 1) NOT NULL,
    [TotalCount]         INT      NOT NULL,
    [SendCount]          INT      DEFAULT ((0)) NOT NULL,
    [ValidityEnd]        DATETIME NOT NULL,
    [PreOrder19DianId]   BIGINT   NOT NULL,
    CONSTRAINT [PK_COUPONSENDDETAIL] PRIMARY KEY CLUSTERED ([CouponSendDetailID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵价券发送明细', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponSendDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponSendDetail', @level2type = N'COLUMN', @level2name = N'CouponSendDetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送总量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponSendDetail', @level2type = N'COLUMN', @level2name = N'TotalCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'已发数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponSendDetail', @level2type = N'COLUMN', @level2name = N'SendCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期截至', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponSendDetail', @level2type = N'COLUMN', @level2name = N'ValidityEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点单编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponSendDetail', @level2type = N'COLUMN', @level2name = N'PreOrder19DianId';

