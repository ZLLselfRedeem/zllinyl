CREATE TABLE [dbo].[CouponGetDetail] (
    [CouponGetDetailID]     INT            IDENTITY (1, 1) NOT NULL,
    [CouponDetailNumber]    NVARCHAR (100) NOT NULL,
    [GetTime]               DATETIME       NOT NULL,
    [ValidityEnd]           DATETIME       NOT NULL,
    [RequirementMoney]      FLOAT (53)     NOT NULL,
    [DeductibleAmount]      FLOAT (53)     NOT NULL,
    [State]                 INT            NOT NULL,
    [MobilePhoneNumber]     NVARCHAR (50)  NOT NULL,
    [CouponId]              INT            NOT NULL,
    [UseTime]               DATETIME       NULL,
    [PreOrder19DianId]      BIGINT         NULL,
    [IsCorrected]           BIT            NOT NULL,
    [CorrectTime]           DATETIME       NULL,
    [SharePreOrder19DianId] BIGINT         NOT NULL,
    [OriginalNumber]        NVARCHAR (50)  NULL,
    [CheckTime] DATETIME NULL , 
    CONSTRAINT [PK_COUPONGETDETAIL] PRIMARY KEY CLUSTERED ([CouponGetDetailID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵价券明细', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'CouponGetDetailID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵价券编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'CouponDetailNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'领用时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'GetTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期截止', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'ValidityEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'条件金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'RequirementMoney';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已纠错', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'IsCorrected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'纠错时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'CorrectTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'分享点单编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'SharePreOrder19DianId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原号码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'OriginalNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵扣金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'DeductibleAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'手机号码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'MobilePhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵价券ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'CouponId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'UseTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点单编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CouponGetDetail', @level2type = N'COLUMN', @level2name = N'PreOrder19DianId';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'查看时间',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'CouponGetDetail',
    @level2type = N'COLUMN',
    @level2name = N'CheckTime'