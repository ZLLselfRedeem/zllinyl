CREATE TABLE [dbo].[Coupon] (
    [CouponId]         INT            IDENTITY (1, 1) NOT NULL,
    [CouponName]       NVARCHAR (100) NOT NULL,
    [ValidityPeriod]   INT            NOT NULL,
    [StartDate]        DATETIME       NOT NULL,
    [EndDate]          DATETIME       NOT NULL,
    [SheetNumber]      INT            NOT NULL,
    [SendCount]        INT            NULL,
    [ShopId]           INT            NOT NULL,
    [RequirementMoney] FLOAT (53)     NOT NULL,
    [SortOrder]        INT            NOT NULL,
    [DeductibleAmount] FLOAT (53)     NOT NULL,
    [State]            INT            NOT NULL,
    [CreatedBy]        INT            NOT NULL,
    [CreateTime]       DATETIME       NOT NULL,
    [LastUpdatedBy]    INT            NULL,
    [LastUpdatedTime]  DATETIME       NULL,
    [Remark]           NVARCHAR (200) NULL,
    [RefuseReason]     NVARCHAR (200) NULL,
    [AuditEmployee]    INT            NULL,
    [AuditTime]        DATETIME       NULL,
    CONSTRAINT [PK_COUPON] PRIMARY KEY CLUSTERED ([CouponId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵价券', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'CouponId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵价券名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'CouponName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'ValidityPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动起始日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动截至时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后更新人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'LastUpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'LastUpdatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'SheetNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送数量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'SendCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属门店', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'ShopId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'条件金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'RequirementMoney';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'SortOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'抵扣金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Coupon', @level2type = N'COLUMN', @level2name = N'DeductibleAmount';

